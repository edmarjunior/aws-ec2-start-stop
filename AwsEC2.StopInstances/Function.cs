using System.Threading.Tasks;
using Amazon.Lambda.Core;
using AwsEC2.Operations.EC2;
using AwsEC2.Operations.Shared;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AwsEC2.StopInstances
{
    public class Function
    {
        public async Task FunctionHandler(ILambdaContext context)
        {
            LambdaLogger.Log($"Executing the { context.FunctionName } function with a { context.MemoryLimitInMB }MB limit.");

            var helper = new EC2OperationsHelper();

            var describeOperation = await helper.GetInstancesByTag(Constants.AUTO_STOP_TAG);
            LambdaLogger.Log(describeOperation.OperationReport);

            var changeOperation = await helper.StopEC2InstancesByInstanceIds(describeOperation.InstanceIds);
            LambdaLogger.Log(changeOperation.OperationReport);

            LambdaLogger.Log($"Finished executing the { context.FunctionName } function.");
        }
    }
}
