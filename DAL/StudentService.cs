using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class StudentService
    {
        /// <summary>
        /// 获取所有学生列表
        /// </summary>
        /// <returns></returns>
        public List<Student> getAll()
        {
            List<Student> resultList = new List<Student>();
            string sql = "select * from student";
            MySqlDataReader reader = SQLHelper.getReader(sql);
            while (reader.Read())
            {
                Student student = new Student()
                {
                    studentName = reader["studentName"].ToString(),
                    classId = Convert.ToInt32(reader["classId"]),
                    birthday = Convert.ToDateTime(reader["birthday"]),
                    img = reader["img"].ToString(),
                    sex = reader["sex"].ToString()
                };
                resultList.Add(student);
            }
            return resultList;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public bool insert(Student student)
        {
            string sql = "insert into student (classId, birthday, img, studentName, sex) values ('{0}', '{1}', '{2}', '{3}', '{4}')";
            sql = string.Format(sql, student.classId, student.birthday, student.img, student.studentName, student.sex);
            List<string> sqlList = new List<string>();
            sqlList.Add(sql);
            bool isSuccess = SQLHelper.ExecuteTransaction(sqlList);
            return isSuccess;
        }

    }
}
