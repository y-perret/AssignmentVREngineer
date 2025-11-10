# Reaction Time Experiment

This repository contains a Unity application that simulates reaction training. It takes the shape of a small "whack-a-mole" game. Data is recorded and sent to a minimal .NET backend.

---

## Unity Application

### Description
The Unity app runs reaction time trials, collects response data, and posts events and summaries to the backend.

### How to Run from Project
1. Open the project in **Unity (6000.2.10f1)**.  
2. Open the main scene: `Assets/Scenes/ReactionTraining.unity`.  
3. Press **Play** to start a session.  
4. Make sure the backend server is running (see below).

### How to Run from executable
1. Run **AssignmentMindMetrix.exe**.

---

## Local Backend

### Description
A minimal **ASP.NET Core Web API** that receives trial events and session summaries. The data is then saved in `events.log` and `summaries.log` files at the root of the folder.

### Requirements
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)

### Run Locally
```bash
cd Backend/ReactionBackend
dotnet run
```

---

## How to play
1. Click on the **Start** button
2. Press on the spacebar or the left mouse button when a mole appears
3. Complete the 10 trials

---

## Scripts structure
- `ReactionTrainingManager.cs`: Main script managing the overall flow of the app and calling the other components. It also contains all parameters related to the trials.
- `SignalSimulator.cs`: Simulate a continuous signal by providing a signal sample at a constant interval
- `DataLogger.cs`: Log signal sample, trial event and session summary and send them to the backend
- `InputManager.cs`: Manages user input
- `SignalSample.cs`: Data model for generated simulated signal sample
- `SessionSummary.cs`: Data model for session summary
- `TrialEvent.cs`: Data model for trial event
- `NetworkClient.cs`: Http client handling network requests to the server
- `NetworkConfig.cs`: ScriptableObject holding server configuration
- `ReactionDataNetworkService.cs`: Sends trial events and session summary to the backend using a network client
- `StimuliManager.cs`: Manage all the visual side (stimuli) of the activity
- `Stimuli.cs`: Represents a single stimulus and handles its behaviour
- `UIManager.cs`: Handles all the UI (starting button and feedback)

---

## Data flow

### Overview
The application records both discrete trial events and continuous simulated signals. The signal simulation generates `SignalSample` every 100 ms.
Each trial generates structured data (trial event) that is aggregated into a session summary, then sent to the backend. The session summary is also written locally in the `/Assets` folder.

Each trial proceeds as follows:

1. **Inter-Trial Interval**  
   A random pause between **1.0 and 1.5 seconds** occurs before the stimulus appears.  
   - If the user reacts during this time, the trial is marked as **`TooEarly` (1)**.

2. **Stimulus Presentation**  
   After the interval, a **stimulus (mole)** appears at a random position.

3. **Reaction Window**  
   The user has up to **2 seconds** to respond.  
   - If an input is detected within the window, the trial is marked as **`Reacted` (3)**.  
   - If no input occurs within the window, the trial is marked as **`TooLate` (2)**.

4. **Data Logging**  
   The trial event data, including timestamps, reaction time, signal samples, and status is recorded by the `DataLogger`.

5. **Feedback Phase**  
   A brief **1-second** feedback is displayed before the next trial begins.

---

## Data Structure Examples

### Trial Event

```json
{
  "trialIndex": 0,
  "trialStartMs": 1762799828598,
  "stimuliStartMs": 1762799829927,
  "stimulusType": "Mole",
  "trialStatus": 2,
  "reactionTimeMs": -1,
  "signalSamples": [
    {
      "seq": 1,
      "timestampMs": 1762799828703,
      "value": 0.9709963202476502
    },
    {
      "seq": 2,
      "timestampMs": 1762799828805,
      "value": 0.9634127020835877
    },
    {
      "seq": 3,
      "timestampMs": 1762799828908,
      "value": 1.0
    }
  ]
}
```

### Session Summary

```json
{
    "sessionId": "ed8b687e-3d36-4aea-933f-d3a0ef7d7b89",
    "sessionStartMs": 1762803948372,
    "sessionEndMs": 1762803968238,
    "totalTrials": 10,
    "averageReactionTimeMs": 260,
    "pctMissedResponses": 0.4000000059604645,
    "averageSignalValue": 0.5141348242759705,
    "trials": [
        {
            "trialIndex": 0,
            "trialStartMs": 1762803948374,
            "stimuliStartMs": 1762803949662,
            "stimulusType": "Mole",
            "trialStatus": 3,
            "reactionTimeMs": 489,
            "signalSamples": [
                {
                    "seq": 1,
                    "timestampMs": 1762803948475,
                    "value": 0.9414288401603699
                },
                ...
            ]
        },
        ...
    ]
}
```

### Backend APIs

| Method | Endpoint                  | Description               |
| ------ | --------------------------| ------------------------- |
| `POST` | `/api/reaction/events`    | Receives trial event data |
| `POST` | `/api/reaction/summaries` | Receives session summary  |

## Limitations and design choices

Due to time limitation, I was not able to add different types of stimuli. This is why the trial events contain a field `stimulusType` not really used.

Continuous signal simulation data is only sent after each trial. If "live" values are needed, they should be sent separately. The use of Websockets could also be an alternative for that.

Depending on the refresh rate of the simulated data and the duration of the sessions, it might be necessary to optimize the size of the generated data.

The networking part would require additional work to ensure a better reliability (retry, timeout, network checks,...)

Each timestamp is recorded in Unix epoch time to improve backend compatibility and prevent time-zone issues.

## Notes on AI assistance
AI has been used to create the minimal ASP.net backend and for the formatting of the README.