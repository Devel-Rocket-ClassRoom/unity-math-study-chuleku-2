using UnityEngine;
using System.IO;

public class FileStream : MonoBehaviour
{
    void Start()
    {
        string path = Path.Combine(Application.persistentDataPath, "Save", "player.json");

        Debug.Log($"전체 경로: {path}");
        Debug.Log($"파일명: {Path.GetFileName(path)}");
        Debug.Log($"파일명(확장자 제외): {Path.GetFileNameWithoutExtension(path)}");
        Debug.Log($"확장자: {Path.GetExtension(path)}");
        Debug.Log($"디렉터리: {Path.GetDirectoryName(path)}");
    }
}
