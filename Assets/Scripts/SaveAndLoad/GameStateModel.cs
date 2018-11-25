using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using System.IO;
using System.Security.Cryptography;
using System.Text;
//https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.rijndaelmanaged?view=netframework-4.7.2

using Newtonsoft.Json;


public class GameStateModel : ISavable {
	public SerializableVector3 lastPosition;
	public Dictionary<string, int> itemList;	//아이템 코드와 갯수
	public float currentHP;

	private static string EncryptionKey = "Banana";
    private static readonly byte[] SALT = new byte[] { 0x26, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc5, 0xfe, 0x07, 0xaf, 0x4d, 0x08, 0x22, 0x3c };
    //https://stackoverflow.com/questions/6482883/how-to-generate-rijndael-key-and-iv-using-a-passphrase

	static public T Deserialize<T>(byte[] data) where T : ISavable
	{
        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey,SALT);

        string str = DecryptStringFromBytes(data,pdb.GetBytes(32), pdb.GetBytes(16));
		T obj = JsonConvert.DeserializeObject<T>(str);
        return obj;
	}
    static public void SerializeAndMakeFile(string fileName , GameStateModel model)
    {
        byte[] data = Serialize(model);
        byte[] metaData = Serialize(GenerateMetaFile());
        FlushToFile(fileName, data, metaData);
    }
	static public byte[] Serialize(GameStateModel model)
	{
		string data = JsonConvert.SerializeObject(model, typeof(GameStateModel), null);

        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey,SALT);
        return EncryptStringToBytes(data, pdb.GetBytes(32), pdb.GetBytes(16));
    }
    static public byte[] Serialize(SaveMeta meta)
    {
        string data = JsonConvert.SerializeObject(meta, typeof(GameStateModel), null);

        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey,SALT);
        return EncryptStringToBytes(data, pdb.GetBytes(32), pdb.GetBytes(16));
    }

	static public void FlushToFile(string fileName, byte[] data, byte[] metaData)
	{
		string path = GetSaveLocation(fileName);
		BinaryWriter writer = new BinaryWriter(File.Create(path));
        using(writer)
		{
			writer.Write(data);
		}

        string metaPath = GetSaveMetaLocation(fileName);
        BinaryWriter metaWriter = new BinaryWriter(File.Create(metaPath));
        using(metaWriter)
		{
			metaWriter.Write(metaData);
		}
	}

    static public string GetSaveLocation(string fileName)
    {
        return Application.dataPath + "/" + fileName + ".bin"; //TODO : 배포할 때 DataPath 변경 필요
    }

    static public string GetSaveMetaLocation(string fileName)
    {
        return Application.dataPath + "/" + fileName + ".metaBin"; 
    }

    static private SaveMeta GenerateMetaFile()
    {
        //TODO : 아래가 디버그 코드임 (임시 코드임) 수정할 것 
        SaveMeta meta = new SaveMeta();
        meta.savedTime = DateTime.Now;
        meta.locationAtSavedTime = "NOWHERE";
        meta.FireDungeonCleared = false;
        meta.WaterAndElectricityDungeonCleared = false;
        meta.WindDungeonCleared = false;

        return meta;
    }
	static private byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
    {
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException("plainText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");
        byte[] encrypted;

        using (RijndaelManaged rijAlg = new RijndaelManaged())
        {
            rijAlg.Key = Key;
            rijAlg.IV = IV;

            ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }
        return encrypted;
    }

    static private string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
    {
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException("cipherText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");


        string plaintext = null;

        using (RijndaelManaged rijAlg = new RijndaelManaged())
        {
            rijAlg.Key = Key;
            rijAlg.IV = IV;

            ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }

        }
        return plaintext;
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }

    public class SerializableVector3
	{
		public float x;
		public float y;
		public float z;

        public SerializableVector3()
        {
            x = 0 ; y = 0 ; z = 0 ;
        }
        public SerializableVector3(Vector3 vector)
        {
            CopyFromVector3(vector);
        }

        public void CopyFromVector3(Vector3 vector)
        {
            x = vector.x; y = vector.y ; z = vector.z;
        }
	}

    public class SaveMeta : ISavable
    {
        public DateTime savedTime;
        public string locationAtSavedTime;
        public bool FireDungeonCleared;
        public bool WaterAndElectricityDungeonCleared;
        public bool WindDungeonCleared;
    }


}
