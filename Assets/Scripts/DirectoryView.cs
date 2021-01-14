using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectoryView : MonoBehaviour
{
    [SerializeField]
    Button showAllBtn;
    
    [SerializeField]
    Button clearDataBtn;
    
    [SerializeField]
    Button searchByNameBtn;
    
    [SerializeField]
    Button searchByNumberBtn;
    
    [SerializeField]
    Button addBtn;
    
    [SerializeField]
    Button removeBtn;

    [SerializeField]
    InputField numberField;

    [SerializeField]
    InputField nameField;

    [SerializeField]
    InputField addressField;

    DirectoryData directoryData;
    
    void Start()
    {
        directoryData = new DirectoryData();
        directoryData.LoadData();
        
        addBtn.onClick.AddListener(AddElement);
        clearDataBtn.onClick.AddListener(ClearData);
        removeBtn.onClick.AddListener(RemoveElement);
        searchByNameBtn.onClick.AddListener(SearchByName);
        searchByNumberBtn.onClick.AddListener(SearchByNumber);
        showAllBtn.onClick.AddListener(ShowAllElements);
    }

    void ShowAllElements()
    {
        var data = directoryData.Data;
        if (data.Count != 0)
        {
            foreach (var citizenData in data)
                Debug.Log($"{citizenData.Value.name} проживает: {citizenData.Value.address}, номер телефона: {citizenData.Key}");
        }
        else
            Debug.Log("Справочник пуст");
    }

    void RemoveElement()
    {
        if (!String.IsNullOrEmpty(numberField.text))
        {
            if (directoryData.RemoveCitizenData(numberField.text))
                Debug.Log($"{numberField.text} удалён из справочника.");
            
            ClearFields();
        }
    }

    void AddElement()
    {
        if (!String.IsNullOrEmpty(numberField.text) &&
            !String.IsNullOrEmpty(nameField.text) &&
            !String.IsNullOrEmpty(addressField.text))
        {
            if (directoryData.AddCitizenData(numberField.text, nameField.text, addressField.text))
            {
                Debug.Log($"{nameField.text} добавлен/а в справочник");
                ClearFields();
            }
            else
                Debug.Log($"Номер {numberField.text} уже есть в справочнике.");
        }
    }

    void SearchByName()
    {
        if (!String.IsNullOrEmpty(nameField.text))
        {
            var citizens = directoryData.SearchByName(nameField.text);

            if (citizens != null)
            {
                foreach (var citizen in citizens)
                    Debug.Log($"Гражданин {citizen.Value.name}, проживает по адресу {citizen.Value.address}, имеет номер {citizen.Key}.");
            }
            else
                Debug.Log($"В справочнике нету никого с именем {nameField.text}");
        }
    }

    void SearchByNumber()
    {
        if (!String.IsNullOrEmpty(numberField.text))
        {
            var citizen = directoryData.SearchByNumber(numberField.text);
            if(citizen != null)
                Debug.Log($"Номер {numberField.text} принадлежит гражданину с имененм: {citizen.name}, проживающего по адресу: {citizen.address}.");
            else
                Debug.Log($"В справочнике не содержится номера {numberField.text}");
        }
    }

    void ClearData()
    {
        directoryData.ClearData();
        Debug.Log("Справочник очищен");
    }

    void ClearFields()
    {
        numberField.text = null;
        nameField.text = null;
        addressField.text = null;
    }
}
