using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServerApplication
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        //Bổ sung
        [OperationContract]
        List<Student> GetStudents();
        [OperationContract]
        Student GetStudent(string id);
        [OperationContract]
        List<Student> FindStudent(string name);
        [OperationContract]
        bool AddNewStudent(Student student);
        [OperationContract]
        bool UpdateStudent(Student student);
        [OperationContract]
        bool DeleteStudent(string id);
    }
}
