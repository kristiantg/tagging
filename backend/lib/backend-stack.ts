import * as cdk from 'aws-cdk-lib';
import { Construct } from 'constructs';
import * as ecs from 'aws-cdk-lib/aws-ecs';
import * as ecsp from 'aws-cdk-lib/aws-ecs-patterns';
import ec2 = require('aws-cdk-lib/aws-ec2');

export class BackendStack extends cdk.Stack {
  constructor(scope: Construct, id: string, props?: cdk.StackProps) {
    super(scope, id, props);


    // Create VPC and Fargate Cluster
    // NOTE: Limit AZs to avoid reaching resource quotas
    const vpc = new ec2.Vpc(scope, 'MyVpc', { maxAzs: 2 });
    const cluster = new ecs.Cluster(scope, 'Cluster', { vpc });

    new ecsp.ApplicationLoadBalancedFargateService(this, 'MyWebServer', {
      cluster,
      taskImageOptions: {
        image: ecs.ContainerImage.fromAsset("../TaggingToolApi"),
      },
      publicLoadBalancer: true
    });
  }
}