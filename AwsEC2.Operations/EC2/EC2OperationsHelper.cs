using Amazon.EC2;
using Amazon.EC2.Model;
using AwsEC2.Operations.Extensions;
using AwsEC2.Operations.Models;
using AwsEC2.Operations.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AwsEC2.Operations.EC2
{
    public class EC2OperationsHelper
    {
        public async Task<DescribeEC2Operation> GetInstancesByTag(string tag)
        {
            var describeOperation = new DescribeEC2Operation();

            try
            {
                using var ec2Client = new AmazonEC2Client();

                var describeResponse = await ec2Client.DescribeInstancesAsync(new DescribeInstancesRequest
                {
                    Filters = new List<Filter> { new Filter("tag-key", new List<string> { tag }) }
                });

                if (describeResponse?.Reservations?.Count > 0)
                {
                    describeResponse.Reservations.ForEach(reservation =>
                    {
                        if (reservation?.Instances?.Count > 0)
                        {
                            reservation.Instances.ForEach(instance =>
                            {
                                describeOperation.InstanceIds.Add(instance.InstanceId);
                            });
                        }
                    });
                }

                describeOperation.OperationReport = describeResponse != null
                    ? describeResponse.HttpStatusCode.GetStatusMessageFromHttpStatusCode()
                    : Constants.NULL_RESPONSE_MESSAGE;
            }
            catch (Exception ex)
            {
                describeOperation.OperationReport = ex.ToFriendlyExceptionString();
            }

            return describeOperation;
        }

        public async Task<ManipulateEC2Operation> StartEC2InstancesByInstanceIds(List<string> instanceIds)
        {
            var changeOperation = new ManipulateEC2Operation();

            try
            {
                using var ec2Client = new AmazonEC2Client();

                var startResponse = await ec2Client.StartInstancesAsync(new StartInstancesRequest(instanceIds));

                // Set the OperationReport property for logging purposes (to be handled by the caller) - details how this operation went
                changeOperation.OperationReport = startResponse != null
                    ? startResponse.HttpStatusCode.GetStatusMessageFromHttpStatusCode()
                    : Constants.NULL_RESPONSE_MESSAGE;
            }
            catch (Exception ex)
            {
                changeOperation.OperationReport = ex.ToFriendlyExceptionString();
            }

            return changeOperation;
        }

        public async Task<ManipulateEC2Operation> StopEC2InstancesByInstanceIds(List<string> instanceIds)
        {
            var changeOperation = new ManipulateEC2Operation();

            try
            {
                using var ec2Client = new AmazonEC2Client();
               
                var stopResponse = await ec2Client.StopInstancesAsync(new StopInstancesRequest(instanceIds));

                // Set the OperationReport property for logging purposes (to be handled by the caller) - details how this operation went
                changeOperation.OperationReport = stopResponse != null
                    ? stopResponse.HttpStatusCode.GetStatusMessageFromHttpStatusCode()
                    : Constants.NULL_RESPONSE_MESSAGE;
            }
            catch (Exception ex)
            {
                changeOperation.OperationReport = ex.ToFriendlyExceptionString();
            }

            return changeOperation;
        }
    }
}
