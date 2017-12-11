using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSyntax
{
    class Program
    {
        static void Main(string[] args)
        {
            C1003GroupBy();

            Console.ReadKey();
        }

        #region 0.0 帮助集合
        static List<Person> GetPersonList()
        {
            return new List<Person>()
            {
                new Person(){Id=4, Name ="张三",Age = 12,TypeId=1},
                new Person(){Id=6, Name ="李四",Age = 100,TypeId=3},
                new Person(){Id=5, Name ="王五",Age = 12,TypeId=2},
                new Person(){Id=3, Name ="赵六",Age = 34,TypeId=1},
                new Person(){Id=2, Name ="宋七",Age = 200,TypeId=4},
                new Person(){Id=1, Name ="周八",Age = 1,TypeId=1},
            };
        }

        static List<WorkType> GetWorkTypeList()
        {
            return new List<WorkType>()
            {
                new WorkType(){ TypeId=1, TypeName="程序员"},
                new WorkType(){ TypeId=2, TypeName="公务员"},
                new WorkType(){ TypeId=3, TypeName="教师"},
                new WorkType(){ TypeId=4, TypeName="司机"}
            };
        }
        #endregion

        #region 1.0 自动属性

        #endregion

        #region 2.0 var的用法
        static void C02Var()
        {
            var person = new Person();
        }
        #endregion

        #region 3.0 匿名类
        /// <summary>
        /// 小结：
        /// 1、匿名类的写法 new { key = value的形式定义你想要的属性}
        /// 2、匿名类本质上被编译器生成了一个泛型类，在此类的构造函数中给相应的字符赋值
        /// 3、当两个匿名类的参数名称一样，个数一样，但是类型不一样 共用一个泛型类
        /// 4、当两个匿名类的个数不一样或者参数名称不一样必须重新生成各自的泛型类
        /// 5、注意 匿名类必须要赋初始值
        /// </summary>
        static void C03AnmClass()
        {
            //正常类的写法
            Person person = new Person();

            //匿名类的写法
            var dog = new { Name = "小花", Age = 1, ToyName = "皮球", };

            var dog1 = new { Name = 1, Age = 1, ToyName = "皮球", };

            var dog2 = new { Name = 1, Age = 1, };

            var dog3 = new { Age = 1, Name = 1, };

            var dog4 = new { Name111 = 1, Age = 1, };

            Console.WriteLine(dog.Name + "," + dog.Age + "," + dog.ToyName);
        }
        #endregion

        #region 4.0 参数默认值 和 命名参数
        static void C04DefaultValue()
        {
            C0401("boy", 22);
            C0401(); //编译器会自动根据C0401方法中的参数默认值补全此处的写法：C0401("tom",22);
            C0401("joy");
            C0401(age: 20);
        }

        static void C0401(string name = "tom", int age = 22)
        {
            Person person = new Person();

            person.Age = age;
            person.Name = name;
        }
        #endregion

        #region 5.0 对象、集合 初始化器
        static void C05ObjectAndArrayInit()
        {
            //对象
            Person person = new Person();

            person.Name = "job";

            Person p = new Person()
            {
                Name = "123",
                Age = 20
            };

            //集合
            var list = new List<Person>
            {
                new Person(){ Name = "123",Age = 20},
                new Person(){ Name = "123",Age = 20},
            };

            //数组
            Person[] perArr = new Person[4];
            perArr[0] = new Person();

            Person[] perArr02 = new Person[]
            {
                new Person(),
                new Person(),
                new Person(),
            };

        }
        #endregion

        #region 6.0 匿名方法（匿名函数、匿名委托） 和 Lambad表达式的推断
        //定义了一个整数入参，返回类型为bool的委托
        public delegate bool DeleTest(int num);

        //定义一个和委托具有相同签名的方法
        static bool Prcss(int num)
        {
            return num > 3;
        }

        static void C06AnmMethod()
        {
            //1.0 匿名方法的写法规则 delegate(参数类型 参数名称){方法体代码}

            //2.0 利用和委托Predicate<T>具有相同签名的方法进行查询
            var list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //需求：从list中找出大于3的所有元素，以新集合返回
            var nlist = list.FindAll(Prcss);
            foreach (var item in nlist)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("--------------------------------");

            //3.0 利用匿名委托实现上面的需求
            var n2list = list.FindAll(delegate (int num) { return num > 3; });

            foreach (var item in n2list)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// 需求：从Person对象集合中查找出年龄大于20，并且名称中包含m的对象，以新集合返回
        /// </summary>
        static void C0601AnnTest()
        {
            //1.0 定义集合
            var list = new List<Person>()
            {
                new Person(){ Name = "tom", Age = 23},
                new Person(){ Name = "boy", Age =18},
                new Person(){ Name = "toc", Age = 29}
            };

            //2.0 利用FindAll方法实现 
            //2.0.1 定义自己的方法与Predcate<T>具有相同签名的方法where进行查询
            var nlist = list.FindAll(where);
            foreach (var item in nlist)
            {
                Console.WriteLine(item);
            }

            //2.0.2 利用匿名函数进行查询
            var nlist2 = list.FindAll(delegate (Person p) { return p.Age > 20 && p.Name.Contains("m"); });
            foreach (var item in nlist2)
            {
                Console.WriteLine(item);
            }

            //2.0.3 lambda表达式的推断
            //第一步演变
            // => goes to
            var nlist3 = list.FindAll((Person p) => { return p.Age > 20 && p.Name.Contains("m"); });

            //第二步演变
            var nlist4 = list.FindAll(p => p.Age > 20 && p.Name.Contains("m"));

            /*
             * 总结：
             *  1、如果参数只有一个，则可以省略小括号，只留下形参名称，如果参数数量大于一个，则必须保留小括号，例如(person,car) => true
             *  2、当方法体只有一句代码的时候，可以省略大括号和最后的分号，如果有返回值的可以省略return关键字
             *  3、当方法体有多句代码的时候，不可以省略大括号，如果有返回值的也不能省略return
             */

            var nlist5 = list.FindAll(p =>
            {
                p.Age = p.Age + 10;
                return p.Age > 20 && p.Name.Contains("m");
            });
        }

        static bool where(Person p)
        {
            return p.Age > 20 && p.Name.Contains("m");
        }
        #endregion

        #region 7.0 扩展方法
        /// <summary>
        /// 扩展方法的要素：
        ///1、此方法必须是一个静态方法
        ///2、此方法必须放在静态类中
        ///3、此方法的第一个参数必须以this开头，并且指定此方法是扩展自哪个类型上的
        ///下面告诉特点和本质
        ///4、扩展方法扩展自哪个类型，就必须是此类型的变量来点,其他类型无法使用
        ///5、扩展方法中的this后面的参数不属于 方法的参数
        ///6、如果扩展方法和实例方法具有相同的签名，则优先调用实例方法
        ///7、扩展自父类上的方法，可以被子类的对象直接使用
        ///8、扩展自接口上的扩展方法，可以被实现类的对象直接使用
        ///9、扩展方法的本质，最终还是被编译器编译成了 静态类.静态方法()
        /// </summary>
        static void C07ExtMethod()
        {
            DateTime now = DateTime.Now;
            string fmtStr1 = now.ToString("yyyy-MM-dd HH:mm:ss");
            string fmtStr2 = now.ToString("yyyy-MM-dd");
            //使用一般方法
            string fmtStr3 = ExtHelper.FmtDate(now);

            //使用扩展方法
            //此种写法是 语法糖 最终被编译成了 ExtHelper.FmtDate(now)
            string fmtStr4 = now.FmtDate();
        }
        #endregion

        #region 8.0 系统内置委托

        #region 8.0.1 Action 接受参数 无返回值
        static void C0801Action()
        {
            var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };

            //需求 输出集合中的元素

            //使用最原始的方法
            //list.ForEach(C0801Method);

            //使用匿名委托
            //list.ForEach(delegate (int i) { Console.WriteLine(i); });

            //使用labmada表达式
            //list.ForEach(i => Console.WriteLine(i));
        }

        static void C0801Method(int i)
        {
            Console.WriteLine(i);
        }

        #endregion

        #region 8.0.2 Predicate 接受一个参数， 返回一个bool值
        static void C0802Predicate()
        {
            var list = GetPersonList();

            var nlist = list.FindAll(p => p.Age > 20);

            nlist.ForEach(c => Console.WriteLine(c.Age));

            var person = list.Find(p => p.Age > 20);

            Console.WriteLine(person.Age);
        }
        #endregion

        #region 8.0.3 Func 接受参数 返回参数，但是不固定
        static void C0803Func()
        {
            var list = GetPersonList();
            //需求：查找list中年龄大于30的所有人的集合
            var nlist = list.Where(p => p.Age > 30).ToList();
            var nlist2 = list.FindAll(p => p.Age > 30);

            nlist.ForEach(c => Console.WriteLine(c.ToString()));
            Console.WriteLine("------------------------");
            nlist2.ForEach(c => Console.WriteLine(c.ToString()));
        }
        #endregion

        #region 8.0.4 Comparison 返回一个整数 接受两个同类型的参数
        static void C0804Comparison()
        {
            var list = GetPersonList();

            //需求:将list中的所有对象按照年龄倒序排列
            //list.Sort(C0804Sort);

            //list.Sort(delegate (Person p1, Person p2) { return p1.Age - p2.Age; });

            list.Sort((p1, p2) => p1.Age - p2.Age);

            list.ForEach(p => Console.WriteLine(p.ToString()));
        }


        static int C0804Sort(Person p1, Person p2)
        {
            return p1.Age - p2.Age;
        }
        #endregion
        #endregion

        #region 9.0 SQO方法 - 标准查询运算符

        #region 9.0.1 Where() FirstOrDefult() LastOrDefault方法进行查找操作
        static void C0901()
        {
            var list = GetPersonList();

            //利用where查找出list中年龄大于5 或者名称等于小猪的
            //链式编程
            list.Where(p => p.Age > 12 || p.Name.Contains("王")).ToList().ForEach(p => Console.WriteLine(p.ToString()));

            Console.WriteLine("---------------------------");

            //FirstOrDefault
            var person = list.FirstOrDefault(p => p.Age > 12 || p.Name.Contains("王"));
            Console.WriteLine(person.ToString());

            Console.WriteLine("---------------------------");

            var person1 = list.LastOrDefault(p => p.Age > 12 || p.Name.Contains("王"));
            Console.WriteLine(person1.ToString());
        }
        #endregion

        #region 9.0.2 对集合进行排序(正序和倒叙)
        static void C0902OrderBy()
        {
            var list = GetPersonList();

            list.OrderBy(c => c.Age).ToList().ForEach(c => Console.WriteLine(c.ToString()));

            Console.WriteLine("----------------------------");

            list.OrderByDescending(c => c.Age).ToList().ForEach(c => Console.WriteLine(c.ToString()));

        }
        #endregion

        #region 9.0.3 对集合进行多个字段排序
        /*
         * 总结：
         * 1、对一个集合进行单个字段的升序排列使用：OrderBy  降序:OrderByDescending
         * 2、对一个集合进行多个字段排列：的一个字段使用OrderBy  或OrderByDescending之一
         *      第2个字段或者以后的字段则使用ThenBy和ThenByDescending之一
         */
        static void C0903ThenBy()
        {
            var list = GetPersonList();
            //需求，先根据Age倒序后再根据id倒序
            //Sql语句：select * from list order by Age desc,ID desc

            //list.OrderBy(p => p.Age).OrderBy(p => p.Id).ToList().ForEach(p => Console.WriteLine(p.ToString()));

            list.OrderBy(p => p.Age).ThenByDescending(p => p.Id).ToList().ForEach(p => Console.WriteLine(p.ToString()));
        }
        #endregion

        #region 9.0.4 投影方法 Select
        static void C0904Select()
        {
            var list = GetPersonList();
            //需求1：将list集合中的Name属性的值获取成一个新的集合
            List<string> nlist = list.Select(p => p.Name).ToList();
            nlist.ForEach(s => Console.WriteLine(s));

            //需求2：将list集合中的 Name和Age属性获取成一个新的对象的集合

            var nlist2 = list.Select(p => new { p.Name, p.Age }).ToList();

            nlist2.ForEach(p => Console.WriteLine(p.ToString()));
        }

        #endregion

        #region 9.0.5 分页方法 Skip Take
        static void C0905SkipTake()
        {
            var list = GetPersonList();

            //跳过第一行 获取两行
            var nlist = list.Skip(1).Take(2).ToList();
            nlist.ForEach(p => Console.WriteLine(p.ToString()));

            Console.WriteLine("___________");

            int total;
            var nlist2 = C0905Page(3, 2, out total);
            nlist2.ForEach(p => Console.WriteLine(p.ToString()));
        }

        static List<Person> C0905Page(int pageIndex, int pageSize, out int Total)
        {
            var list = GetPersonList();

            Total = list.Count;

            return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
        #endregion

        #region 9.0.6 Join联表操作
        static void C0906Join()
        {
            var plist = GetPersonList();
            var wlist = GetWorkTypeList();

            //需求：查找piglist集合中的Name和ID值同时和typelist中的TName的值作为一个新的匿名类集合返回
            //SQL: select p.Name,p.ID,t.TName from piglist p join typelist t on (p.TypeID=t.TypeID)

            //var nlist = plist.Join(wlist, p => p.TypeId, w => w.TypeId, (p, w) => new { p.Name, p.Age, p.Id, w.TypeId, w.TypeName }).ToList();

            var nlist = plist.Join(wlist, p => p.TypeId, w => w.TypeId, (p, w) => new { p.Name, p.Age, w.TypeId, w.TypeName }).ToList();
            nlist.ForEach(m => Console.WriteLine(m.ToString()));

        }
        #endregion

        #region 9.0.7 GroupBy分组
        static void C0907GroupBy()
        {
            var list = GetPersonList();

            list.GroupBy(p => p.TypeId).ToList().ForEach(c =>
            {
                Console.WriteLine(c.Key);
                c.ToList().ForEach(g => Console.WriteLine(g.ToString()));
            });

        }
        #endregion
        #endregion

        #region 10.0 Linq语法，本质上还是会拆分成SQO方法进行调用

        #region 10.1 用Linq实现Where
        static void C1001Where()
        {
            var list = GetPersonList();

            var nlist = (from p in list
                         where p.Age > 20
                         select p).ToList();

            nlist.ForEach(p => Console.WriteLine(p.ToString()));
        }
        #endregion

        #region 10.0.2 用Linq实现排序
        static void C1002Sort()
        {
            var list = GetPersonList();

            var nlist = (from p in list
                         orderby p.Age descending, p.Id ascending
                         select p).ToList();

            nlist.ForEach(p => Console.WriteLine(p.ToString()));
        }
        #endregion

        #region 10.0.3 用Linq实现集合的链接
        static void C1003Join()
        {
            var plist = GetPersonList();
            var wlist = GetWorkTypeList();

            var nlist = from p in plist
                        join w in wlist on p.TypeId equals w.TypeId
                        select new { p.Name, p.Age, p.TypeId, w.TypeName };

            nlist.ToList().ForEach(c => Console.WriteLine(c.ToString()));
        }
        #endregion

        #region 10.0.4 用Linq实现分组
        static void C1003GroupBy()
        {
            var plist = GetPersonList();

            var nlist = from p in plist
                        group p by p.TypeId;

            nlist.ToList().ForEach(c =>
            {
                Console.WriteLine(c.Key);
                c.ToList().ForEach(p => Console.WriteLine(p.ToString()));
            });
        }
        #endregion
        #endregion
    }

    public static class ExtHelper
    {
        /// <summary>
        /// 扩展方法的要素：
        /// 1、此方法必须是一个静态方法
        /// 2、此方法必须放在静态类中
        /// 3、此方法的第一个参数必须以this开头，并且指定此方法是扩展自哪个类型上的
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string FmtDate(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        //public static string FmtDate(DateTime dt)
        //{
        //    return dt.ToString("yyyy-MM-dd HH:mm:ss");
        //}
    }
}
