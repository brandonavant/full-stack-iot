# Raspberry Pi

The Raspberry Pi is the proxy between the RGB LED and IoT Core. The RPi subscribes to the IoT Core thing's topic and raises an event when a message is published to the topic.

## Preparing the security certs

Before you can run the application, you must copy down the certificate files that you generated when setting up the thing in AWS IoT. You will also need to update the corresponding variables in index.js.

## Running the application

In order to run the application, you will need to either run the command below *or* build a daemon for automatic start-up. The latter is ideal for production environments:

```BASH
sudo node index.js
```

NOTE: Please be advised that you must run the command with `sudo`; the reason for this is that the *pigpio* library need sudoer priviledges to interact with the serial ports.