# Full-Stack IoT Demonstration

The purpose of this repository is to demonstrate a full-stack IoT solution using the following technologies:

- Raspberry Pi
- Amazon Alexa
- NodeJs
- AWS Lambda

## Raspberry Pi

In this probject, we will connect an LED to a Raspberry Pi. We will wire an RGB LED to the RPi and change the color using commands sent from Alexa and/or a front-end application. Because we will be utilizing an RGB LED, we will need to utilize PWM (Pulse-width modulation).

### Materials

In order to implement the RPi setup, you will need the following components:

- A Raspberry Pi running Raspbian, SSH, and Node.js.
- The [pigpio module](https://www.npmjs.com/package/pigpio) for Node.js.
- A Breadboard.
- Three 220 Ohm resistors.
- One RGB LED (this tutorial uses a common cathode).
- Four female-to-male jumper wires.

Note: [This kit](https://www.amazon.com/dp/B01ERPEMAC/ref=cm_sw_r_tw_dp_U_x_7J4CEb4HV4TNT) on Amazon has all of the components that you need (and more) and is pretty cheap!

### Installing pigpio

We will be using the Node.js *pigpio* wrapper, which provides the ability to interact with PWM (pulse-width-modulation) components from our code on the RPi. The install, run the following commands in the given order:

First update the package repository's lists:

```pi@raspberrypi:~ $ sudo apt update```

Next, install the C library on top of which the Node.js pigpio wrapper is built:

```pi@raspberrypi:~ $ sudo apt install pigpio```

Finally, install the wrapper:

```pi@raspberrypi:~ $ npm i pigpio```

Note: When running code that utilizes the pigpio library, you will need root/sudo privileges. This is because the wrapper uses the underlying C library.

### Building the circuit

In order to get our Node.js code to control the LED via PWM, we first need to wire up the LED on a breadboard. The diagram below demonstrates how things should be wired.

![circuit](circuit.png)

## IoT Core

In order to provide connectivity between cloud applications (e.g. Alexa Skills, React apps, etc.) and the Raspberry Pi, we must provision a *Thing* in IoT Core.

### Add a device to the Thing registry

1. Login to the AWS Console and search for IoT Core.
2. Once IoT Core is shown, click *Manage* and then *Things*.
3. Click *Register a thing*.
4. Click *Create a single thing*.
5. Type a meaningful name for the thing (e.g. *Raspberry-Pi*).
6. Skip the other options on the first page and click *Next*.
7. Click *Create certificate*.
8. Download *A certificate for this thing* and *A private Key*. You don't need the public key.
9. Click *A root CA for AWS IoT*, which will navigate you to the *Server Authentication Certificate* page. Scroll down and download *Amazon Root CA 1* or *Amazon Root CA 3*.
10. Store these files somewhere safe on your local machine. 
11. Click Activate.
12. Click Create Policy.

**Note: Never share these files; they are secrets**.

### Creating an IoT Policy

We must define actions for which our device is authorized. Complete the following steps:

1. In the name field, type a meaningful policy name (e.g. Rapberry-Pi-Policy).
2. For *Actions* type `iot:*`.
3. For *Resource ARN* type `*`.
4. For *Effect* check Allow.
5. Click *Create*.

**IMPORTANT NOTE:* We are opening up all actions and resource access. This is insecure for a production environment. For an actual production setup, you will want to provide a minimum rights (i.e. only what's needed).

### Attach Policy to Device Certificate

Now that we've created our policy, we need to attach it to the previously-create certificate.

1. Click Secure->Certificates.
2. Click the ellipses (i.e. ...) and *Attach policy*.
3. Check the new policy and click *Attach*.

### Attach Certificate to Thing

Finally, let's attach the certificate to the Thing.

1. Click Secure->Certificates.
2. Click the ellipses (i.e. ...) and *Attach thing*.
3. Check the Thing and click *Attach*.

## Setting up the RPi Environment

### Cloning the Code

### Environment Variables

In order to pull the unique *IoTCore* environment variables referenced in the code, you will need to export the values in `/etc/profile`. In order to do this, type `sudo nano /etc/profile` and enter the following values at the bottom:

Note: the clientId and endpoint can be found in the following places:

#### ClientId Location

### Endpoint Location
