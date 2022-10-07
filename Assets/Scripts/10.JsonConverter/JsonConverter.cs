﻿using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

public abstract class JsonConverter<T>
{
    //public string CurrentDirectory = Directory.GetCurrentDirectory();
    public string CurrentDirectory;

    public void createJSON(T Object)
    {
        var settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };
        //Debug.Log(Object.ToString());
        var object_string = JsonConvert.SerializeObject(Object, settings);
        File.WriteAllText(CurrentDirectory, object_string);
    }
    public string GetText()
    {
        return File.ReadAllText(CurrentDirectory);
    }
    public void DeleteData()
    {
        File.WriteAllText(CurrentDirectory, "");
    }
    public T getObjectfromText(string t)
    {
        T arr2 = JsonConvert.DeserializeObject<T>(t);
        return arr2;
    }
    public T getObjectFromJSON()
    {
        Debug.Log(CurrentDirectory);
        T arr2 = JsonConvert.DeserializeObject<T>(File.ReadAllText(CurrentDirectory));
        return arr2;
    }
    public void setCurrentDir(string str)
    {
        CurrentDirectory = "jar:file://" + Application.dataPath + "/" + str + "!/assets";
    }
}
