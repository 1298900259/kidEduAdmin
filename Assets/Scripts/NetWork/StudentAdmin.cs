using App.Base;
using App.Controller;
using App.Model;
using AppArchi.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentAdmin : SingletonMono<StudentAdmin>
{
    public string _Host;
    
    public string _User;
    public string _Password;
    public string _Database;

    const string _Host_Cloud = "rm-bp16g83auj30d1n95bo.mysql.rds.aliyuncs.com";
    const string _User_Cloud = "ssykid";
    const string _Password_Cloud = "Ssy_1688";
    const string _Database_Cloud = "kidedu";

    string table = "student_admin";
    string column1 = "`Group`";

    string column2 = "Id";
    string column3 = "Group_Pos";

    DatabaseMgr db;
    // Start is called before the first frame update
    public void Ctor()
    {
        Init();
    }
    public DatabaseMgr GetDB()
    {
        return db;
    }
    /*public void InitExcel()
    {
        MainController.Instance.vw.Btn_Excel.onClick.AddListener(() => {
            MainController.Instance.vw.Btn_Excel.GetComponent<CreateExcel>().SaveExcel(db);
        });
    }*/

    private void Update()
    {
        InitHost();
    }

    public void Init()
    {
        db = new DatabaseMgr();
        db.Bind(_Host, _User, _Password, _Database);
    }
    public void InitHost()
    {
        if(Input.GetKey(KeyCode.Y)&&Input.GetKey(KeyCode.U))
        {
#if UNITY_EDITOR
            Debug.Log("connect cloud");
#endif
            if (PlayerPrefs.HasKey("host"))
            {
                PlayerPrefs.DeleteKey("host");
            }
            if (_Host!=_Host_Cloud)
            {
                _Host = _Host_Cloud;
                _User = _User_Cloud;
                _Password = _Password_Cloud;
                _Database = _Database_Cloud;
                Init();
            }
        }
    }

    public void Commit(string groupName,int id,int group_pos)
    {
        if (db.Select<string, int>(column2, table, column1, groupName, column3, group_pos)==null)
        {
            db.Insert<string, int, int>(table, column1, groupName, column2, id, column3, group_pos);
            return;
        }
        db.Update<int>(table, column2, id, column1, groupName, column3, group_pos);
    }

    public void Commit(StudentInfo info)
    {
        if (db.Select<string, int>(column2, table, column1, info.group, column3, info.group_pos) == null)
        {
            db.Insert<string, int, int>(table, column1, info.group, column2, info.id, column3, info.group_pos);
            return;
        }
        db.Update<int>(table, column2, info.id, column1, info.group, column3, info.group_pos);
    }

    public void Clear()
    {
        db.Clear(table);
    }

}
