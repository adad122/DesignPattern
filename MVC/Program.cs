#region MVC 模式
//MVC 模式代表 Model-View-Controller（模型-视图-控制器） 模式。这种模式用于应用程序的分层开发。

//Model（模型） - 模型代表一个存取数据的对象或 JAVA POJO。它也可以带有逻辑，在数据变化时更新控制器。
//View（视图） - 视图代表模型包含的数据的可视化。
//Controller（控制器） - 控制器作用于模型和视图上。它控制数据流向模型对象，并在数据变化时更新视图。它使视图与模型分离开。
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC
{
    public class Student
    {
        private String _rollNo;
        private String _name;

        public String GetRollNo()
        {
            return _rollNo;
        }
        public void SetRollNo(String rollNo)
        {
            _rollNo = rollNo;
        }
        public String GetName()
        {
            return _name;
        }
        public void SetName(String name)
        {
            _name = name;
        }
    }

    public class StudentView
    {
        public void OutputStudentDetail(String name, String rollNo)
        {
            Console.WriteLine("Student: ");
            Console.WriteLine(" Name: " + name + " Roll No: " + rollNo);
        }
    }

    public class StudentController
    {
        private readonly Student _studentModal;
        private readonly StudentView _studentViewModal;
        public StudentController(Student modal, StudentView view)
        {
            _studentModal = modal;
            _studentViewModal = view;
        }

        public void SetStudentName(String name)
        {
            _studentModal.SetName(name);
        }

        public String GetStudentName()
        {
            return _studentModal.GetName();
        }

        public void SetStudentRollNo(String rollNo)
        {
            _studentModal.SetRollNo(rollNo);
        }

        public String GetStudentRollNo()
        {
            return _studentModal.GetRollNo();
        }

        public void UpdateView()
        {
            _studentViewModal.OutputStudentDetail(_studentModal.GetName(), _studentModal.GetRollNo());
        }  
    }
    class Program
    {
        private static Student RetriveStudentFromDatabase()
        {
            Student student = new Student();
            student.SetName("Robert");
            student.SetRollNo("10");
            return student;
        }

        static void Main(string[] args)
        {
            Student modal = RetriveStudentFromDatabase();

            StudentView view = new StudentView();

            StudentController controller = new StudentController(modal, view);

            controller.UpdateView();

            controller.SetStudentName("Jhon");

            controller.UpdateView();
        }
    }
}
