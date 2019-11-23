# full-stack-iot

The purpose of this repository is to demonstrate to RSI colleagues (and all of the Internet) ways in which a developer can build a full-stack IoT solution using the following technologies:

- React.js - The front-end will utilize React to provide a SPA for our demo app.
- Socket.io-client - Since updates from our back-end shall be distributed to the front-end immediately (without constant polling), we want to utilize the WebSocket protocol. Socket.io will provide us with this capability in a nice Managed library.
- Node.js - Since our front-end is 100% JavaScript, what better than to use JavaScript on the back-end as well? The back-end will provide both a RESTful API and socket.io connectivity for our React app. This layer is also tasked with reading from the Event Hubs data produced by IoT Hub.
- C#/.NET Core 3.0 - For the time being, we will utilize C#/.NET Core 3.0 on our Raspberry Pi to communicate with the hardware components. This layer will report data to Azure IoT Hub via the Azure SDK.
- Raspberry Pi - I'll be using a RPi to communicate with the physical hardware from which IoT Hub telemetry is produced.
- PN532 - I will use a PN532 to provide a hardware input. This will allow users to scan an RFID tag and validate the information in Azure.
- LED - I will use a single LED to provide a hardware output. This will allow us to provide visible feedback regarding a pass/fail of the PN532 tag validation.
