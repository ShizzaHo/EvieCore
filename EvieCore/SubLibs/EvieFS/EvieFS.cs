using System;
using System.IO;
using UnityEngine;

public class EvieFS
{
    public static EvieFS Instance { get; private set; }

    public string systemDocumentDir;
    public string systemUserDir;
    public string systemGamePathDir;
    public string systemGamePathFullpath;

    public EvieConfigManager configManager;

    private EvieFS()
    {
        systemDocumentDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        systemUserDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        systemGamePathDir = Application.persistentDataPath;
        systemGamePathFullpath = Path.Combine(systemGamePathDir, Application.productName);

        configManager = new EvieConfigManager(this);
    }

    public static void Initialize()
    {
        if (Instance == null)
        {
            Instance = new EvieFS();
        }
    }

    public void CreateFile(string dirPath, string fileName)
    {
        string fullPath = Path.Combine(dirPath, fileName);
        if (!File.Exists(fullPath))
        {
            File.Create(fullPath).Dispose(); // Создаёт файл и освобождает ресурсы
        }
        else
        {
            Debug.LogError($"[EVIECORE/SYBLIBS/EVIEFS/ERROR] The {fileName} file already exists in the {dirPath} directory.");
        }
    }

    public void CreateFile(string dirPath, string fileName, string fileContent)
    {
        string fullPath = Path.Combine(dirPath, fileName);
        if (!File.Exists(fullPath))
        {
            File.WriteAllText(fullPath, fileContent);
        }
        else
        {
            Debug.LogError($"[EVIECORE/SYBLIBS/EVIEFS/ERROR] The {fileName} file already exists in the {dirPath} directory.");
        }
    }

    public void CreateDir(string dirPath, string folderPath)
    {
        string fullDirPath = Path.Combine(dirPath, folderPath);
        if (!Directory.Exists(fullDirPath))
        {
            Directory.CreateDirectory(fullDirPath);
        }
        else
        {
            Debug.LogError($"[EVIECORE/SYBLIBS/EVIEFS/ERROR] The directory {folderPath} already exists in the {dirPath} directory.");
        }
    }

    public void CreateDir(string dirPath)
    {
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        else
        {
            Debug.LogError($"[EVIECORE/SYBLIBS/EVIEFS/ERROR] The directory {dirPath} already exists.");
        }
    }

    public void EditFile(string filePath, string newFileContent)
    {
        if (File.Exists(filePath))
        {
            File.WriteAllText(filePath, newFileContent);
        }
        else
        {
            Debug.LogError($"[EVIECORE/SYBLIBS/EVIEFS/ERROR] The file in the {filePath} path was not found.");
        }
    }

    public void RenameFile(string filePath, string newFileName)
    {
        if (File.Exists(filePath))
        {
            string newFilePath = Path.Combine(Path.GetDirectoryName(filePath), newFileName);
            File.Move(filePath, newFilePath);
        }
        else
        {
            Debug.LogError($"[EVIECORE/SYBLIBS/EVIEFS/ERROR] The file in the {filePath} path was not found.");
        }
    }

    public void RenameFile(string dirPath, string fileName, string newFileName)
    {
        string fullPath = Path.Combine(dirPath, fileName);
        if (File.Exists(fullPath))
        {
            string newFilePath = Path.Combine(dirPath, newFileName);
            File.Move(fullPath, newFilePath);
        }
        else
        {
            Debug.LogError($"[EVIECORE/SYBLIBS/EVIEFS/ERROR] The {fileName} file was not found in the {dirPath} directory.");
        }
    }

    public void MoveFile(string filePath, string newFilePath)
    {
        if (File.Exists(filePath))
        {
            File.Move(filePath, newFilePath);
        }
        else
        {
            Debug.LogError($"[EVIECORE/SYBLIBS/EVIEFS/ERROR] The file in the {filePath} path was not found.");
        }
    }

    public void DeleteFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        else
        {
            Debug.LogError($"[EVIECORE/SYBLIBS/EVIEFS/ERROR] The file in the {filePath} path was not found.");
        }
    }

    public bool isFileExist(string filePath)
    {
        return File.Exists(filePath);
    }

    public bool isDirExist(string dirPath)
    {
        return Directory.Exists(dirPath);
    }
}