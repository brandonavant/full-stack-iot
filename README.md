# Full-Stack IoT Demonstration

## Introduction

The purpose of this repository is to demonstrate to RSI colleagues (and all of the Internet) ways in which a developer can build a full-stack IoT solution using various technologies.

To present a real-world example, I will demonstrate how to utilize an RFID/NFC scanner attempt to authenticate a scanned fob by communicating with the cloud. The scans will instantaneously appear in a log shown on a React application.

- React.js - The front-end will utilize React to provide a SPA for our demo app.
- Socket.io-client - Since updates from our back-end shall be distributed to the front-end immediately (without constant polling), we want to utilize the WebSocket protocol. Socket.io will provide us with this capability in a nice Managed library.
- Node.js - Since our front-end is 100% JavaScript, what better than to use JavaScript on the back-end as well? The back-end will provide both a RESTful API and socket.io connectivity for our React app. This layer is also tasked with reading from the Event Hubs data produced by IoT Hub.
- C#/.NET Core 3.0 - For the time being, we will utilize C#/.NET Core 3.0 on our Raspberry Pi to communicate with the hardware components. This layer will report data to Azure IoT Hub via the Azure SDK.
- Raspberry Pi - I'll be using a RPi to communicate with the physical hardware from which IoT Hub telemetry is produced.
- PN532 - I will use a PN532 to provide a hardware input. This will allow users to scan an RFID tag and validate the information in Azure.
- LED - I will use a single LED to provide a hardware output. This will allow us to provide visible feedback regarding a pass/fail of the PN532 tag validation.

## Architecture

As illustrated in the image below, the RPi is tasked with capturing authentication attempts from users using a physical MiFare card. The corresponding authentication information is JSON-serialized and sent to IoT Hub as a Message. This message is then intercepted by the listening Node.js application, which validates the authentication information against the corresponding record in the CosmosDB database.

The outcome of the validation (pass or fail) is sent back to the RPi via a C2D message. Additionally, the attempt (and the outcome) are sent to the Socket.io-connected React application.

![Architectural Diagram](architecture.png)

## Setup

### Environment Variables

## Deployment

### Deploying the React application

### Deploying the Node service

### Deploying the Firmware to the RPi