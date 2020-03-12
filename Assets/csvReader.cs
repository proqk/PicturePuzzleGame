//using System.Collections;
//using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class csvReader : MonoBehaviour
{
    public List<Tuple<string, int>> Read(string filename) //list[문제 번호]=dic(문제, 정답 인덱스)
    {
        var list = new List<Tuple<string, int>>();
        TextAsset sourcefile = Resources.Load<TextAsset>(filename);
        StringReader sr = new StringReader(sourcefile.text);

        while (sr.Peek() > -1)
        {
            string data_String = sr.ReadLine();

            var data_values = data_String.Split(','); //string, string타입
            if (data_values[0] == "") continue;
            var tmp = new Tuple<string, int>(data_values[0], int.Parse(data_values[1])); //문제, 정답
            list.Add(tmp);
        }
        return list;
    }
}