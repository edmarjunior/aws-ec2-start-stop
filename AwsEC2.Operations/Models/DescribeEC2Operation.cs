using System.Collections.Generic;

namespace AwsEC2.Operations.Models
{
    public class DescribeEC2Operation : BaseOperationModel
    {
        public List<string> InstanceIds { get; set; } = new List<string>();
    }
}
