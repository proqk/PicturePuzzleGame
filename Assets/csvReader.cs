//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using System.IO;

//public class csvReader : MonoBehaviour
//{
//    public List<object> Read(string file)
//    {
//        var list = new List<object>();
//        StreamReader sr = new StreamReader(Application.dataPath + "/" + file);
//        int i = 1; //스테이지는 1부터 시작하게

//        while (!sr.EndOfStream)
//        {
//            string data_String = sr.ReadLine();

//            var data_values = data_String.Split(','); //string, string타입
//            object[] tmp= { };
//            tmp[0] = data_values[0]; //문제
//            tmp[1] = int.Parse(data_values[1]); //정답
//            list.AddRange(tmp);
//        }

//        return list;
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class csvReader : MonoBehaviour
{
    public List<Tuple<string, int>> Read(string file) //list[문제 번호]=dic(문제, 정답 인덱스)
    {
        var list = new List<Tuple<string, int>>();
        TextAsset sourcefile = Resources.Load<TextAsset>("stage");
        StringReader sr = new StringReader(sourcefile.text);

        while (sr.Peek() > -1)
        {
            string data_String = sr.ReadLine();

            var data_values = data_String.Split(','); //string, string타입
            var tmp = new Tuple<string, int>(data_values[0], int.Parse(data_values[1])); //문제, 정답
            list.Add(tmp);
        }

        return list;
    }
}