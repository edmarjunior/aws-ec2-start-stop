using System.Threading.Tasks;
using Amazon.Lambda.Core;
using AwsEC2.Operations.EC2;
using AwsEC2.Operations.Shared;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AwsEC2.StartInstances
{
    public class Function
    {
        public async Task FunctionHandler(ILambdaContext context)
        {
            LambdaLogger.Log($"Executing the { context.FunctionName } function with a { context.MemoryLimitInMB }MB limit.");

            var helper = new EC2OperationsHelper();

            // First, obtain instance ids to start
            var describeOperation = await helper.GetInstancesByTag(Constants.AUTO_START_TAG);
            LambdaLogger.Log(describeOperation.OperationReport);

            // start instances based on the returned ids
            var changeOperation = await helper.StartEC2InstancesByInstanceIds(describeOperation.InstanceIds);
            LambdaLogger.Log(changeOperation.OperationReport);

            LambdaLogger.Log($"Finished executing the { context.FunctionName } function.");
        }
    }
}
