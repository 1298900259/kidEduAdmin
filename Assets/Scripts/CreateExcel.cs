using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using Excel;
using System.Data;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using AppArchi.Networking;
using App.Base;
using System.Diagnostics;

public class InjectSingleScore
{
    public string userId;
    public int userScore;//singleScore
    public int userDays;
    public string userGroup;


    public InjectSingleScore(string userId, int userScore, int userDays,string userGroup)
    {
        this.userId = userId;
        this.userScore = userScore;
        this.userDays = userDays;
        this.userGroup = userGroup;
    }
}
//15days
public class InjectScoreList
{
    public string userId;
    public int firstDayScore;
    public int secondDayScore;
    public int thirdDayScore;
    public int fourthDayScore;
    public int fifthDayScore;
    public int sixthDayScore;
    public int seventhDayScore;
    public int eighthDayScore;
    public int ninthDayScore;
    public int tenthDayScore;
    public int eleventhDayScore;
    public int twelfthDayScore;
    public int thirteenthDayScore;
    public int fourteenthDayScore;
    public int fifteenthDayScore;
    public float averageScore;


    public InjectScoreList(string userId, int firstDayScore, int secondDayScore, int thirdDayScore, int fourthDayScore, int fifthDayScore, int sixthDayScore, int seventhDayScore, int eighthDayScore, int ninthDayScore, int tenthDayScore, int eleventhDayScore, int twelfthDayScore, int thirteenthDayScore, int fourteenthDayScore, int fifteenthDayScore, float averageScore)
    {
        this.userId = userId;
        this.firstDayScore = firstDayScore;
        this.secondDayScore = secondDayScore;
        this.thirdDayScore = thirdDayScore;
        this.fourthDayScore = fourthDayScore;
        this.fifthDayScore = fifthDayScore;
        this.sixthDayScore = sixthDayScore;
        this.seventhDayScore = seventhDayScore;
        this.eighthDayScore = eighthDayScore;
        this.ninthDayScore = ninthDayScore;
        this.tenthDayScore = tenthDayScore;
        this.eleventhDayScore = eleventhDayScore;
        this.twelfthDayScore = twelfthDayScore;
        this.thirteenthDayScore = thirteenthDayScore;
        this.fourteenthDayScore = fourteenthDayScore;
        this.fifteenthDayScore = fifteenthDayScore;
        this.averageScore = averageScore;
    }
}

public class CreateExcel :SingletonMono<CreateExcel>
{
    public void SaveExcel(DatabaseMgr db)
    {
        OpenFileName ofn = new OpenFileName();

        ofn.structSize = Marshal.SizeOf(ofn);
        ofn.filter = "Excel Files(*.xlsx)\0*.xlsx\0";  //指定打开格式

        ofn.file = new string(new char[256]);

        ofn.maxFile = ofn.file.Length;

        ofn.fileTitle = new string(new char[64]);

        ofn.maxFileTitle = ofn.fileTitle.Length;

        ofn.initialDir = UnityEngine.Application.dataPath;//默认路径

        ofn.title = "打开Excel";

        ofn.defExt = "xlsx";

        //注意 一下项目不一定要全选 但是0x00000008项不要缺少
        ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR

        List<string> userScoreStored = new List<string>();

        //打开windows框
        if (DllTest.GetSaveFileName(ofn))
        {
            //TODO

            //把文件路径格式替换一下
            ofn.file = ofn.file.Replace("\\", "/");
            //Debug.Log(ofn.file);

            FileInfo newFile = new FileInfo(ofn.file);
            try
            {
                if (newFile.Exists)
                {
                    newFile.Delete();  // ensures we create a new workbook
                    newFile = new FileInfo(ofn.file);
                }
                using (ExcelPackage package = new ExcelPackage(newFile))
                {
                    // 添加一个sheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("信息");

                    //添加数据
                    List<InjectSingleScore> importClasses = new List<InjectSingleScore>();
                    List<string> userId = new List<string>();
                    List<int> userScore = new List<int>();
                    List<int> userDays = new List<int>();
                    List<string> userGroup = new List<string>();
                    userId = db.Select<string>("id", "account");
                    userScore = db.Select("`singleScore`", "account");
                    userDays = db.Select("`clock in days`", "account");
                    userGroup = db.Select<string>("`group`", "account");

                    for (int num = 0; num < userId.Count; num++)
                    {
                        importClasses.Add(new InjectSingleScore(userId[num], userScore[num], userDays[num], userGroup[num]));
                    }
                    /*importClasses.Add(new ImportClass("小刚", "789", "789789"));
                    importClasses.Add(new ImportClass("小亮", "147", "147147"));*/

                    worksheet.Cells[1, 1, 1, 8].Merge = true;//合并单元格(1行1列到1行6列)
                    worksheet.Cells["A1"].Value = "每日成绩"; //显示
                    worksheet.Cells["A1"].Style.Font.Size = 16; //字体大小
                    worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; //对其方式
                    worksheet.Cells["A1"].Style.Border.BorderAround(ExcelBorderStyle.Thin); //表格边框


                    worksheet.Cells[2, 1, 2, 2].Merge = true;
                    worksheet.Cells["A2"].Value = "学生学号";
                    worksheet.Cells["A2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A2"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells["B2"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                    worksheet.Cells[2, 3, 2, 4].Merge = true;
                    worksheet.Cells["C2"].Value = "成绩";
                    worksheet.Cells["C2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells["C2"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells["D2"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                    worksheet.Cells[2, 5, 2, 6].Merge = true;
                    worksheet.Cells["E2"].Value = "已完成天数";
                    worksheet.Cells["E2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells["E2"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells["F2"].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                    worksheet.Cells[2, 7, 2, 8].Merge = true;
                    worksheet.Cells["G2"].Value = "小组";
                    worksheet.Cells["G2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells["G2"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells["H2"].Style.Border.BorderAround(ExcelBorderStyle.Thin);


                    int i = 3;//第三行才是我们的数据
                    for (int j = 0; j < importClasses.Count; j++)
                    {
                        worksheet.Cells[i, 1, i, 2].Merge = true;
                        worksheet.Cells["A" + i.ToString()].Value = importClasses[j].userId;
                        worksheet.Cells["A" + i.ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["A" + i.ToString()].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        worksheet.Cells["B" + i.ToString()].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                        worksheet.Cells[i, 3, i, 4].Merge = true;
                        if (importClasses[j].userScore == 0)
                            worksheet.Cells["C" + i.ToString()].Value = "/";
                        else
                            worksheet.Cells["C" + i.ToString()].Value = importClasses[j].userScore;
                        worksheet.Cells["C" + i.ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["C" + i.ToString()].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        worksheet.Cells["D" + i.ToString()].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                        worksheet.Cells[i, 5, i, 6].Merge = true;
                        worksheet.Cells["E" + i.ToString()].Value = importClasses[j].userDays;
                        worksheet.Cells["E" + i.ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["E" + i.ToString()].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        worksheet.Cells["F" + i.ToString()].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        
                        worksheet.Cells[i, 7, i, 8].Merge = true;
                        worksheet.Cells["G" + i.ToString()].Value = importClasses[j].userGroup;
                        worksheet.Cells["G" + i.ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["G" + i.ToString()].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        worksheet.Cells["H" + i.ToString()].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                        i++;
                    }
                    package.Save();
                    // 打开Excel文档
                    Process.Start(ofn.file);
                    /*// 关闭Excel文档
                    package.Dispose();*/
                }
            }
            catch (System.Exception)
            {
                UnityEngine.Debug.Log("该Excel文档正在运行中，所以保存失败了");
            }
        }
    }

    //15days
    public void SaveExcelTotal(DatabaseMgr db)
    {
        OpenFileName ofn = new OpenFileName();

        ofn.structSize = Marshal.SizeOf(ofn);
        ofn.filter = "Excel Files(*.xlsx)\0*.xlsx\0";  //指定打开格式

        ofn.file = new string(new char[256]);

        ofn.maxFile = ofn.file.Length;

        ofn.fileTitle = new string(new char[64]);

        ofn.maxFileTitle = ofn.fileTitle.Length;

        ofn.initialDir = UnityEngine.Application.dataPath;//默认路径

        ofn.title = "打开Excel";

        ofn.defExt = "xlsx";

        //注意 一下项目不一定要全选 但是0x00000008项不要缺少
        ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR

        List<string> userScoreStored = new List<string>();

        //打开windows框
        if (DllTest.GetSaveFileName(ofn))
        {
            //TODO

            //把文件路径格式替换一下
            ofn.file = ofn.file.Replace("\\", "/");
            //Debug.Log(ofn.file);

            FileInfo newFile = new FileInfo(ofn.file);
            try
            {
                if (newFile.Exists)
                {
                    newFile.Delete();  // ensures we create a new workbook
                    newFile = new FileInfo(ofn.file);
                }
                using (ExcelPackage package = new ExcelPackage(newFile))
                {
                    // 添加一个sheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("信息");

                    //添加数据
                    List<InjectScoreList> importClasses = new List<InjectScoreList>();
                    #region Get Data
                    List<string> userId = new List<string>();
                    List<int> firstDayScore = new List<int>();
                    List<int> secondDayScore = new List<int>();
                    List<int> thirdDayScore = new List<int>();
                    List<int> fourthDayScore = new List<int>();
                    List<int> fifthDayScore = new List<int>();
                    List<int> sixthDayScore = new List<int>();
                    List<int> seventhDayScore = new List<int>();
                    List<int> eighthDayScore = new List<int>();
                    List<int> ninthDayScore = new List<int>();
                    List<int> tenthDayScore = new List<int>();
                    List<int> eleventhDayScore = new List<int>();
                    List<int> twelfthDayScore = new List<int>();
                    List<int> thirteenthDayScore = new List<int>();
                    List<int> fourteenthDayScore = new List<int>();
                    List<int> fifteenthDayScore = new List<int>();
                    List<int> getScoreNumber = new List<int>();
                    userId = db.Select<string>("id", "scorelist");
                    firstDayScore = db.Select("`firstDayScore`", "scorelist");
                    secondDayScore = db.Select("`secondDayScore`", "scorelist");
                    thirdDayScore = db.Select("`thirdDayScore`", "scorelist");
                    fourthDayScore = db.Select("`fourthDayScore`", "scorelist");
                    fifthDayScore = db.Select("`fifthDayScore`", "scorelist");
                    sixthDayScore = db.Select("`sixthDayScore`", "scorelist");
                    seventhDayScore = db.Select("`seventhDayScore`", "scorelist");
                    eighthDayScore = db.Select("`eighthDayScore`", "scorelist");
                    ninthDayScore = db.Select("`ninthDayScore`", "scorelist");
                    tenthDayScore = db.Select("`tenthDayScore`", "scorelist");
                    eleventhDayScore = db.Select("`eleventhDayScore`", "scorelist");
                    twelfthDayScore = db.Select("`twelfthDayScore`", "scorelist");
                    thirteenthDayScore = db.Select("`thirteenthDayScore`", "scorelist");
                    fourteenthDayScore = db.Select("`fourteenthDayScore`", "scorelist");
                    fifteenthDayScore = db.Select("`fifteenthDayScore`", "scorelist");
                    getScoreNumber = db.GetScoreNum();
                    //averageScore = db.GetScoreNum();
                    #endregion
                    
                    for (int num = 0; num < userId.Count; num++)
                    {
                        int totalScore = firstDayScore[num] + secondDayScore[num] + thirdDayScore[num] + fourthDayScore[num] + fifthDayScore[num] +
                                sixthDayScore[num] + seventhDayScore[num] + eighthDayScore[num] + ninthDayScore[num] + tenthDayScore[num] +
                                eleventhDayScore[num] + twelfthDayScore[num] + thirteenthDayScore[num] + fourteenthDayScore[num] + fifteenthDayScore[num];
                        int number = getScoreNumber[num];
                        float averageScore;
                        if (totalScore == 0)
                            averageScore = 0;
                        else
                            averageScore = (float)totalScore / number;
                        importClasses.Add(new InjectScoreList(
                                userId[num], firstDayScore[num], secondDayScore[num], thirdDayScore[num], fourthDayScore[num], fifthDayScore[num],
                                sixthDayScore[num], seventhDayScore[num], eighthDayScore[num], ninthDayScore[num], tenthDayScore[num],eleventhDayScore[num],
                                twelfthDayScore[num], thirteenthDayScore[num], fourteenthDayScore[num], fifteenthDayScore[num], averageScore));
                    }

                    InjectTitleTotal(worksheet);
                    int i = 3;//第三行才是我们的数据
                    for (int j = 0; j < importClasses.Count; j++)
                    {
                        worksheet.Cells["A" + i.ToString()].Value = importClasses[j].userId;
                        worksheet.Cells["A" + i.ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["A" + i.ToString()].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                        InjectDataTotal(worksheet, "B" + i.ToString(), importClasses[j].firstDayScore);
                        InjectDataTotal(worksheet, "C" + i.ToString(), importClasses[j].secondDayScore);
                        InjectDataTotal(worksheet, "D" + i.ToString(), importClasses[j].thirdDayScore);
                        InjectDataTotal(worksheet, "E" + i.ToString(), importClasses[j].fourthDayScore);
                        InjectDataTotal(worksheet, "F" + i.ToString(), importClasses[j].fifthDayScore);
                        InjectDataTotal(worksheet, "G" + i.ToString(), importClasses[j].sixthDayScore);
                        InjectDataTotal(worksheet, "H" + i.ToString(), importClasses[j].seventhDayScore);
                        InjectDataTotal(worksheet, "I" + i.ToString(), importClasses[j].eighthDayScore);
                        InjectDataTotal(worksheet, "J" + i.ToString(), importClasses[j].ninthDayScore);
                        InjectDataTotal(worksheet, "K" + i.ToString(), importClasses[j].tenthDayScore);
                        InjectDataTotal(worksheet, "L" + i.ToString(), importClasses[j].eleventhDayScore);
                        InjectDataTotal(worksheet, "M" + i.ToString(), importClasses[j].twelfthDayScore);
                        InjectDataTotal(worksheet, "N" + i.ToString(), importClasses[j].thirteenthDayScore);
                        InjectDataTotal(worksheet, "O" + i.ToString(), importClasses[j].fourteenthDayScore);
                        InjectDataTotal(worksheet, "P" + i.ToString(), importClasses[j].fifteenthDayScore);

                        if (importClasses[j].averageScore == 0)
                            worksheet.Cells["Q" + i.ToString()].Value = "/";
                        else
                            worksheet.Cells["Q" + i.ToString()].Value = importClasses[j].averageScore.ToString("F2");
                        worksheet.Cells["Q" + i.ToString()].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["Q" + i.ToString()].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                        i++;
                    }
                    package.Save();
                    // 打开Excel文档
                    Process.Start(ofn.file);
                    /*// 关闭Excel文档
                    package.Dispose();*/
                }
            }
            catch (System.Exception)
            {
                UnityEngine.Debug.Log("该Excel文档正在运行中，所以保存失败了");
            }
        }
    }

    public void InjectTitleTotal(ExcelWorksheet worksheet)
    {
        worksheet.Cells[1, 1, 1, 17].Merge = true;//合并单元格(1行1列到1行6列)
        worksheet.Cells["A1"].Style.Font.Size = 16; //字体大小
        SetCell(worksheet, "A1", "带班十五天成绩", 1, 14);
        SetCell(worksheet, "A2", "学生学号", 2, 14);
        SetCell(worksheet, "B2", "第一天成绩", 3, 14);
        SetCell(worksheet, "C2", "第二天成绩", 4, 14);
        SetCell(worksheet, "D2", "第三天成绩", 5, 14);
        SetCell(worksheet, "E2", "第四天成绩", 6, 14);
        SetCell(worksheet, "F2", "第五天成绩", 7, 14);
        SetCell(worksheet, "G2", "第六天成绩", 8, 14);
        SetCell(worksheet, "H2", "第七天成绩", 9, 14);
        SetCell(worksheet, "I2", "第八天成绩", 10, 14);
        SetCell(worksheet, "J2", "第九天成绩", 11, 14);
        SetCell(worksheet, "K2", "第十天成绩", 12, 14);
        SetCell(worksheet, "L2", "第十一天成绩", 13, 14);
        SetCell(worksheet, "M2", "第十二天成绩", 14, 14);
        SetCell(worksheet, "N2", "第十三天成绩", 15, 14);
        SetCell(worksheet, "O2", "第十四天成绩", 16, 14);
        SetCell(worksheet, "P2", "第十五天成绩", 17, 14);
        SetCell(worksheet, "Q2", "平均成绩", 18, 14);
    }

    public void SetCell(ExcelWorksheet worksheet,string cell,string cellText,int column,int width)
    {
        worksheet.Cells[cell].Value = cellText;
        worksheet.Cells[cell].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;//对其方式
        worksheet.Cells[cell].Style.Border.BorderAround(ExcelBorderStyle.Thin);//表格边框
        worksheet.Column(column).Width = width;//设置第column列列宽
        //worksheet.Row(1).Height = 30;//设置行高
    }
    public void InjectDataTotal(ExcelWorksheet worksheet, string cell, int score)
    {
        if (score == 0)
            worksheet.Cells[cell].Value = "/";
        else
            worksheet.Cells[cell].Value = score;
        worksheet.Cells[cell].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        worksheet.Cells[cell].Style.Border.BorderAround(ExcelBorderStyle.Thin);
    }
}
