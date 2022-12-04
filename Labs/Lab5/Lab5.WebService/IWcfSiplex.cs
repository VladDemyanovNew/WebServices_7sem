using System.Runtime.Serialization;
using System.ServiceModel;

namespace Lab5.WebService
{
    [ServiceContract]
    public interface IWcfSiplex
    {
        [OperationContract]
        int Add(int x, int y);

        [OperationContract]
        string Concat(string s, double d);

        [OperationContract]
        A Sum(A a1, A a2);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "Lab5.WebService.ContractType".
    [DataContract]
    public class A
    {
        [DataMember]
        public string S { get; set; }

        [DataMember]
        public int K { get; set; }

        [DataMember]
        public float F { get; set; }
    }
}
