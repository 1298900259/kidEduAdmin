using UnityEngine;
using AppArchi.Networking;
using App.Repository;
using App.Controller;

public class AccounDb : MonoBehaviour
{
    public string _Host;
    public string _User;
    public string _Password;
    public string _Database;
    string table="account";
    string column1="user";
    string column2="password";

    DatabaseMgr db;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        Login("1", "520");
    }

    void Init()
    {
        db = new DatabaseMgr();
        db.Bind(_Host, _User, _Password, _Database);
    }

    public void Login(string user, string pwd)
    {
        string realPwd = db.Select<string>(column2, table, column1, user);
        if (realPwd == null)
        {
            Debug.Log("The account doesnt exist");         
            return; 
        }
        if (pwd == realPwd)
        {
            Debug.Log("Correct Pwd");           
        } 
        else
        {
             Debug.Log("Wrong Pwd");           
        }       
    }

    public void SignUp(string user, string pwd)
    {
        string str=db.Select<string>(column2,table,column1,user);
        if (str!=null)
        {
             Debug.Log("Database already had the account of same name!");    
             return;
        }
        if (db.Insert<string,string>(table,"user",user,"password",pwd))
        {
            Debug.Log("SignUp!");         
        }
    }

}
