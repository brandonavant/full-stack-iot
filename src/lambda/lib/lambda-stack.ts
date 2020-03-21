import * as cdk from '@aws-cdk/core';
import * as lambda from '@aws-cdk/aws-lambda';
import * as iam from '@aws-cdk/aws-iam';

export class LambdaStack extends cdk.Stack {
  constructor(scope: cdk.Construct, id: string, props?: cdk.StackProps) {
    super(scope, id, props);

    const lambdaFunc = new lambda.Function(this, 'ChangeLightRequestHandler', {
      runtime: lambda.Runtime.NODEJS_10_X,
      code: lambda.Code.fromAsset('functions'),
      handler: 'changeLightRequest.handler',
    });

    lambdaFunc.addToRolePolicy(new iam.PolicyStatement({
      effect: iam.Effect.ALLOW,
      actions: [ 'iot:*' ],
      resources: [ '*' ]
    }));
  }
}
