using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFORM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region 1.0 EF初体验

        private void button1_Click(object sender, EventArgs e)
        {
            //1.实例化EF上下文对象
            WorkRecordDBEntities db = new WorkRecordDBEntities();

            //2.查询用户表中性别为男的所有数据
            var sql = db.UserInfo.Where(u => u.Sex == 0);

            //3.将sql语句发送给数据库执行，获取返回结果集
            var list = sql.ToList();

            //4.打印查询到的结果
            list.ForEach(u => Console.WriteLine(u.UserName + "," + u.Sex + "," + u.Email));
        }
        #endregion

        #region 2.0 新增操作

        private void button2_Click(object sender, EventArgs e)
        {
            //给UserInfo表中新增一条数据
            //1.0 实例化EF上下文容器对象
            WorkRecordDBEntities db = new WorkRecordDBEntities();

            //2.0 创建一个用户实体
            var model = new UserInfo()
            {
                Email = "lbrunner@163.com",
                Sex = 0,
                UserName = "lbrunner",
                CreateDate = DateTime.Now
            };

            //3.0 将Model实体追加到EF容器中，同时产生代理类 状态为Added
            db.UserInfo.Add(model);

            //4.0 通知EF容器，将所有的sql命令发送到数据库
            db.SaveChanges();

            MessageBox.Show("新增成功");
        }


        #endregion

        #region 3.0 批量新增数据
        private void button3_Click(object sender, EventArgs e)
        {
            WorkRecordDBEntities db = new WorkRecordDBEntities();
            UserInfo model;

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            #region 方法一 1281
            for (int i = 0; i < 500; i++)
            {
                model = new UserInfo()
                {
                    CreateDate = DateTime.Now,
                    Email = string.Format("lbrunner{0}@163.com", i),
                    Sex = 1,
                    UserName = string.Format("lbrunner{0}", i),
                };

                db.UserInfo.Add(model);

                db.SaveChanges();
            }
            #endregion

            #region 方法二 516
            //for (int i = 0; i < 500; i++)
            //{
            //    model = new UserInfo()
            //    {
            //        CreateDate = DateTime.Now,
            //        Email = string.Format("lbrunner{0}@163.com", i),
            //        Sex = 1,
            //        UserName = string.Format("lbrunner{0}", i),
            //    };

            //    db.UserInfo.Add(model);
            //}

            //db.SaveChanges();
            #endregion

            stopwatch.Stop();

            MessageBox.Show("执行插入500条数据，耗时" + stopwatch.ElapsedMilliseconds.ToString() + "毫秒");
        }
        #endregion

        #region 4.0 删除数据的几种方式

        private void button4_Click(object sender, EventArgs e)
        {
            WorkRecordDBEntities db = new WorkRecordDBEntities();

            var model = new UserInfo() { Id = 1026 };

            #region 1.0 将实体附加到EF容器中，然后调用Remove方法将状态修改为Deleted，最后保存操作
            ////此时的状态是Unchanged
            //db.UserInfo.Attach(model);
            ////此时的状态是Deleted
            //db.UserInfo.Remove(model);
            //db.SaveChanges();
            #endregion

            #region 2.0 将实体附加到EF容器中,然后手动修改状态为Deleted，最后保存
            //System.Data.Entity.Infrastructure.DbEntityEntry entityEntry = db.Entry<UserInfo>(model);
            //entityEntry.State = EntityState.Deleted;
            //db.SaveChanges();
            #endregion

            #region 3.0 先查后删除
            var list = db.UserInfo.Where(u => u.UserName == "lbrunner83");

            if (list != null & list.Any())
            {
                foreach (var item in list)
                {
                    db.UserInfo.Remove(item);
                }
            }
            db.SaveChanges();
            #endregion
            MessageBox.Show("数据删除成功");
        }
        #endregion

        #region 5.0 编辑数据的几种方式

        private void button5_Click(object sender, EventArgs e)
        {
            WorkRecordDBEntities db = new WorkRecordDBEntities();

            #region 1.0 先查后改 
            //var model = db.UserInfo.Where(m => m.Id == 1003).FirstOrDefault();

            //model.UserName = "python";
            //model.Email = "python@163.com";

            #endregion

            #region 2.0 自定义一个实体，将要修改的属性重新赋值，将状态修改为Modified
            var model = new UserInfo()
            {
                Id = 1003,
                UserName = "我要学Python"
            };

            // 2.0.1 将Model追加到EF容器中，获取代理类对象
            System.Data.Entity.Infrastructure.DbEntityEntry dbEntity = db.Entry(model);

            dbEntity.State = EntityState.Unchanged;
            dbEntity.Property(nameof(UserInfo.UserName)).IsModified = true;
                
            //手动关闭EF对实体验证
            db.Configuration.ValidateOnSaveEnabled = false;

            #endregion
            db.SaveChanges();

            MessageBox.Show("数据编辑成功");
        }
        #endregion

    }
}
