using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hwyeom.Exam
{
    class Program
    {
        static void Main(string[] args)
        {
            string solutionFolder = AppDomain.CurrentDomain.BaseDirectory;
            string dataFolder = solutionFolder + @"\Data";
            string fileName = string.Empty;
            string textJson = string.Empty;

            if (Directory.Exists(dataFolder))
            {
                //폴더 있음
                //요구사항2 User 에서읽은데이터를JSON 객체로변환하는 메소드개발, 이때JObject객체로처리
                fileName = "user.json";
                string jsonFileFullPath = Path.Combine(dataFolder, fileName);
                if (File.Exists(jsonFileFullPath))
                {
                    //파일 있음
                    JObject userJsonObj = ConvertUserToJsonObj(jsonFileFullPath);
                    Console.WriteLine(userJsonObj.ToString());
                    //요구사항4 User 클래스를생성하고JSON에서읽은데이터를User 클래스로변환하는메소드개발
                    User user = UserDeserial(jsonFileFullPath);
                    Console.WriteLine(user.ToString());
                }
                else
                {
                    //파일 없음
                    Console.WriteLine($"'{fileName}'이 없습니다.");
                }

                //요구사항1 Users 에서읽은데이터를JSON 객체로변환하는메소드개발, 이때JArray객체로처리
                fileName = "users.json";
                jsonFileFullPath = Path.Combine(dataFolder, fileName);
                if (File.Exists(jsonFileFullPath))
                {
                    JArray userJsonArray = ConvertUsersToJsonArray(jsonFileFullPath);
                    foreach (var a in userJsonArray)
                        Console.WriteLine(a.ToString());

                    //요구사항3 Users 클래스를생성하고JSON에서읽은데이터를Users 클래스로변환하는메소드개발
                    List<User> users = UsersDeserial(jsonFileFullPath);
                    foreach (var a in users)
                        Console.WriteLine(a.ToString());
                }
                else
                {
                    //파일 없음
                    Console.WriteLine($"'{fileName}'이 없습니다.");
                }
            }
            else
            {
                //폴더 없음
                Console.WriteLine($"폴더 '{dataFolder}' 가 없습니다.");
                //return;
            }


            Console.ReadLine();
        }


        /// <summary>
        /// 요구사항1 Users 에서읽은데이터를JSON 객체로변환하는메소드개발, 이때JArray객체로처리
        /// </summary>
        /// <param name="jsonFileFullPath">Json text파일경로</param>
        /// <returns>JArray</returns>
        private static JArray ConvertUsersToJsonArray(string jsonFileFullPath)
        {
            if (jsonFileFullPath == null)
                return null;

            Encoding encoding = DetectFileEncoding(File.ReadAllBytes(jsonFileFullPath));
            string textJson = File.ReadAllText(jsonFileFullPath, encoding);

            JObject parseJObject = JObject.Parse(textJson);
            //Users.json Json Array Parse
            if (parseJObject.ContainsKey("Users"))
            {
                return JArray.Parse(parseJObject["Users"].ToString());
            } else
                return null;
        }

        /// <summary>
        /// 요구사항2 User 에서읽은데이터를JSON 객체로변환하는 메소드개발, 이때JObject객체로처리
        /// </summary>
        /// <param name="jsonFileFullPath">json파일경로</param>
        /// <returns>JObject</returns>
        private static JObject ConvertUserToJsonObj(string jsonFileFullPath)
        {
            if (jsonFileFullPath == null)
                return null;

            Encoding encoding = DetectFileEncoding(File.ReadAllBytes(jsonFileFullPath));
            string textJson = File.ReadAllText(jsonFileFullPath, encoding);
            return JObject.Parse(textJson);
        }
        
        /// <summary>
        /// 요구사항3 Users 클래스를생성하고JSON에서읽은데이터를Users 클래스로변환하는메소드개발
        /// </summary>
        /// <param name="jsonFileFullPath"></param>
        /// <returns></returns>
        private static List<User> UsersDeserial(string jsonFileFullPath)
        {
            if (jsonFileFullPath == null)
                return null;

            Encoding encoding = DetectFileEncoding(File.ReadAllBytes(jsonFileFullPath));
            string textJson = File.ReadAllText(jsonFileFullPath, encoding);
            JObject parseJsonObj = JObject.Parse(textJson);

            return JsonConvert.DeserializeObject<List<User>>(parseJsonObj["Users"].ToString());
        }

        /// <summary>
        /// 요구사항4 User 클래스를생성하고JSON에서읽은데이터를User 클래스로변환하는메소드개발
        /// </summary>
        /// <param name="jsonFileFullPath">JsonFile경로</param>
        /// <returns></returns>
        private static User UserDeserial(string jsonFileFullPath)
        {
            if (jsonFileFullPath == null)
                return null;

            Encoding encoding = DetectFileEncoding(File.ReadAllBytes(jsonFileFullPath));
            string textJson = File.ReadAllText(jsonFileFullPath, encoding);
            JObject parseJsonObj = JObject.Parse(textJson);
            return JsonConvert.DeserializeObject<User>(parseJsonObj["User"].ToString());
        }


        /// <summary>
        /// [파일 인코딩 체크]
        /// </summary>
        /// <param name="fileBytes"></param>
        /// <returns>Encoding</returns>
        private static Encoding DetectFileEncoding(byte[] fileBytes)
        {
            // Detect the encoding using a BOM (byte order mark)
            if (fileBytes.Length >= 3 && fileBytes[0] == 0xEF && fileBytes[1] == 0xBB && fileBytes[2] == 0xBF)
            {
                return Encoding.UTF8;
            }
            else if (fileBytes.Length >= 4 && fileBytes[0] == 0x00 && fileBytes[1] == 0x00 && fileBytes[2] == 0xFE && fileBytes[3] == 0xFF)
            {
                return Encoding.UTF32; // UTF-32BE
            }
            else if (fileBytes.Length >= 2 && fileBytes[0] == 0xFE && fileBytes[1] == 0xFF)
            {
                return Encoding.BigEndianUnicode; // UTF-16BE
            }
            else if (fileBytes.Length >= 2 && fileBytes[0] == 0xFF && fileBytes[1] == 0xFE)
            {
                if (fileBytes.Length >= 4 && fileBytes[2] == 0 && fileBytes[3] == 0)
                {
                    return Encoding.UTF32;
                }
                else
                {
                    return Encoding.Unicode; // UTF-16LE
                }
            }
            else
            {
                return Encoding.Default;
            }
        }

    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public DateTime CreateDateTime { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"--------------------------------------");
            sb.AppendLine($"Id: {Id}");
            sb.AppendLine($"Name: {Name}");
            sb.AppendLine($"Role: {Role}");
            sb.AppendLine($"CreateDateTime: {CreateDateTime}");
            sb.AppendLine($"--------------------------------------");
            return sb.ToString();
        }
    }


    public class SampleJson
    {
        public string id { get; set; }
        public List<Children> children { get; set; }
        public Job currentJob { get; set; }
        public List<Job> jobs { get; set; }
        public double maxRunDistance { get; set; }
        public string cpf { get; set; }
        public string cnpj { get; set; }
        public string pretendSalary { get; set; }
        public string age { get; set; }
        public string gender { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string hairColor { get; set; }

        public bool CheckAllNullValue()
        {
            return (id == null && children == null && currentJob == null && jobs == null && maxRunDistance == 0 && cpf == null && cnpj == null && pretendSalary == null
                && age == null && gender == null && firstName == null && lastName == null && phone == null && address == null && hairColor == null) ;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"id:{id}");
            foreach(var a in children)
                sb.Append($"children => {a.ToString()}");

            foreach (var a in jobs)
                sb.Append($"children => {a.ToString()}");

            sb.AppendLine($"currentJob:{currentJob.ToString()}");
            sb.AppendLine($"maxRunDistance:{maxRunDistance}");
            sb.AppendLine($"cpf:{cpf}");
            sb.AppendLine($"cnpj:{cnpj}");
            sb.AppendLine($"pretendSalary:{pretendSalary}");
            sb.AppendLine($"age:{age}");
            sb.AppendLine($"gender:{gender}");
            sb.AppendLine($"firstName:{firstName}");
            sb.AppendLine($"lastName:{lastName}");
            sb.AppendLine($"phone:{phone}");
            sb.AppendLine($"address:{address}");
            sb.AppendLine($"hairColor:{hairColor}");

            return sb.ToString();
        }
    }

    public class Children
    {
        public string name { get; set; }
        public int age { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"name:{name}");
            sb.AppendLine($"age:{age}");

            return sb.ToString();
        }
    }

    public class Job
    {
        public string title { get; set; }
        public string salary { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"name:{title}");
            sb.AppendLine($"age:{salary}");

            return sb.ToString();
        }
    }

}