const awsIot = require('aws-iot-device-sdk');
const Gpio = require('pigpio').Gpio;

const redLed = new Gpio(4, { mode: Gpio.OUTPUT });
const greenLed = new Gpio(17, { mode: Gpio.OUTPUT });
const blueLed = new Gpio(27, { mode: Gpio.OUTPUT });

const device = awsIot.device({
  keyPath: '<IoTCorePrivateKeyPath>',
  certPath: '<IoTCoreCertPath>',
  caPath: '<IoTCoreCaPath>',
  clientId: '<IoTCoreClientId>',
  host: 'IoTCoreEndpoint'
});

device.on('connect', () => {
  console.log('Connected!');

  device.subscribe('topic1');
});

device.on('message', (topic, payload) => {
  const rgb = JSON.parse(payload);

  redLed.pwmWrite(rgb.red);
  greenLed.pwmWrite(rgb.green);
  blueLed.pwmWrite(rgb.blue);

});