# Full-Stack IoT CDK Project

The CDK drives the deployment of both the AWS Lambda function, which acts as the endpoint for the Alexa Skill Kit, and the API Gateway endpoint, which acts as a proxy between the HTTPS requests that the ASK makes and the Lambda function.

The actual Lambda function's code can be found in `functions/changeLightRequest.js`.

## Useful commands

 * `npm run build`   compile typescript to js
 * `npm run watch`   watch for changes and compile
 * `npm run test`    perform the jest unit tests
 * `cdk deploy`      deploy this stack to your default AWS account/region
 * `cdk diff`        compare deployed stack with current state
 * `cdk synth`       emits the synthesized CloudFormation template
