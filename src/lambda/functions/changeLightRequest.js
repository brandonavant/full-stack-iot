const AWS = require('aws-sdk');
const Alexa = require('ask-sdk-core');

const iotData = new AWS.IotData({
  endpoint: process.env.iotCoreEndpoint
});

const colorMap = {
  red: {
    red: 255,
    green: 0,
    blue: 0
  },
  green: {
    red: 0,
    green: 255,
    blue: 0
  },
  blue: {
    red: 0,
    green: 0,
    blue: 255
  },
  orange: {
    red: 255,
    green: 165,
    blue: 0
  },
  yellow: {
    red: 255,
    green: 255,
    blue: 0
  },
  purple: {
    red: 128,
    green: 0,
    blue: 128
  }
};

const LaunchRequestHandler = {
  canHandle(handlerInput) {
    return (
      Alexa.getRequestType(handlerInput.requestEnvelope) === 'LaunchRequest'
    );
  },
  handle(handlerInput) {
    return (
      handlerInput.responseBuilder
        .speak('Launch Request Handler')
        // .reprompt(speakOutput)
        .getResponse()
    );
  }
};

const HelloWorldIntentHandler = {
  canHandle(handlerInput) {
    return (
      Alexa.getRequestType(handlerInput.requestEnvelope) === 'IntentRequest' &&
      Alexa.getIntentName(handlerInput.requestEnvelope) === 'color_changer'
    );
  },
  handle(handlerInput) {
    const color = Alexa.getSlotValue(handlerInput.requestEnvelope, 'color');
    const rgb = colorMap[color];
    const responseMessage = rgb
      ? `Changing color to ${color}.`
      : `Sorry, the color ${color}, is not a color that I recognize.`;

    let rgbJson;

    if (rgb) {
      rgbJson = JSON.stringify(rgb);

      iotData.publish(
        { topic: 'topic1', payload: rgbJson, qos: 0 },
        (err, data) => {
          if (err) {
            console.error(err);
          } else {
            console.log('Color set...');
          }
        }
      );
    }

    return (
      handlerInput.responseBuilder
        .speak(responseMessage)
        //.reprompt('add a reprompt if you want to keep the session open for the user to respond')
        .getResponse()
    );
  }
};

exports.handler = Alexa.SkillBuilders.custom()
  .addRequestHandlers(LaunchRequestHandler, HelloWorldIntentHandler)
  .lambda();
