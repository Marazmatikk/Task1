using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class DirectoryAutoTest : MonoBehaviour
{
    DirectoryData directoryData = new DirectoryData(false);

    string  testKey   = "8-999-66-55-404";
    Citizen testValue = new Citizen("Test Testov", "Street Test");


    [Test]
    public void AddElementTest()
    {
        directoryData.AddCitizenData(testKey, testValue);
        var data = directoryData.GetDirectoryCopy();
        if (data.ContainsKey(testKey))
        {
            var citizen = data[testKey];
            Assert.IsTrue(citizen.name == testValue.name);
            Assert.IsTrue(citizen.address == testValue.address);
        }
    }

    [Test]
    public void SearchByNumberTest()
    {
        var data = directoryData.GetDirectoryCopy();
        if (data.ContainsKey(testKey))
            Assert.IsTrue(directoryData.SearchByNumber(testKey) != null);
        else
        {
            AddElementTest();
            Assert.IsTrue(directoryData.SearchByNumber(testKey) != null);
        }
    }

    [Test]
    public void SearchByNameTest()
    {
        var data = directoryData.GetDirectoryCopy();
        if (data.ContainsKey(testKey))
            Assert.IsTrue(directoryData.SearchByName(testValue.name) != null);
        else
        {
            AddElementTest();
            Assert.IsTrue(directoryData.SearchByName(testValue.name) != null);
        }        
    }
    
    [Test]
    public void RemoveElementTest()
    {
        var data = directoryData.GetDirectoryCopy();
        if (data.ContainsKey(testKey))
        {
            directoryData.RemoveCitizenData(testKey);
            Assert.IsTrue(!data.ContainsKey(testKey));
        }
        else
        {
            AddElementTest();
            directoryData.RemoveCitizenData(testKey);
            Assert.IsTrue(!data.ContainsKey(testKey));
        }
    }
}
