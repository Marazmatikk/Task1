using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using UnityEngine;

	[Serializable]
	public class Citizen
	{
		public string name;
		public string address;

		public Citizen(string name, string address)
		{
			this.name    = name;
			this.address = address;
		}
	}

	[Serializable]
	public class CitizensData
	{
		public List<string>  dataKeys   = new List<string>();
		public List<Citizen> dataValues = new List<Citizen>();
	}

public class DirectoryData
{
	string dataPath = Application.dataPath + "/Resources/DirectoryData.json";

	Dictionary<string, Citizen> data = new Dictionary<string, Citizen>();
	public ReadOnlyDictionary<string, Citizen> Data => new ReadOnlyDictionary<string, Citizen>(data);

	bool needSave;

	public DirectoryData(bool needSave)
	{
		this.needSave = needSave;
	}

	public void AddCitizenData(string key, Citizen citizen)
	{
		AddCitizenData(key, citizen.name, citizen.address);
	}

	public bool AddCitizenData(string key, string name, string address)
	{
		if (!data.ContainsKey(key))
		{
			var citizenData = new Citizen(name, address);
			data.Add(key, citizenData);
			SaveData();
			return true;
		}

		return false;
	}

	public bool RemoveCitizenData(string key)
	{
		if (data.ContainsKey(key))
		{
			data.Remove(key);
			SaveData();
			return true;
		}

		return false;
	}

	public Citizen SearchByNumber(string number)
	{
		if (data.ContainsKey(number))
			return new Citizen(data[number].name, data[number].address);

		return null;
	}

	public Dictionary<string, Citizen> SearchByName(string name)
	{
		Dictionary<string, Citizen> citizensDataByName = new Dictionary<string, Citizen>();
		foreach (var citizenData in data)
		{
			if (citizenData.Value.name.Equals(name))
				citizensDataByName.Add(citizenData.Key, citizenData.Value);
		}

		if (citizensDataByName.Count != 0)
			return citizensDataByName;

		return null;
	}

	public void LoadData()
	{
		var dataJson = Resources.Load<TextAsset>("DirectoryData");
		if (dataJson)
		{
			var loadedData = JsonUtility.FromJson<CitizensData>(dataJson.text);
			if (loadedData != null)
				for (int i = 0; i < loadedData.dataKeys.Count; i++)
					AddCitizenData(loadedData.dataKeys[i], loadedData.dataValues[i]);
		}
	}

	public Dictionary<string, Citizen> GetDirectoryCopy()
	{
		var copy = data;
		return copy;
	}

	public void ClearData()
	{
		data.Clear();
		File.Delete(dataPath);
	}

	void SaveData()
	{
		if (needSave)
		{
			CitizensData saveData = new CitizensData();
			saveData.dataKeys   = data.Keys.ToList();
			saveData.dataValues = data.Values.ToList();
			string jsonData = JsonUtility.ToJson(saveData);
			File.WriteAllText(dataPath, jsonData);
		}
	}
}
