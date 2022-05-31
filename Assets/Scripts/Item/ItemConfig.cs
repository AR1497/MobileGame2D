using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 0)]
public class ItemConfig : ScriptableObject
{
    [SerializeField]
    public int id;

    [SerializeField]
    public string _title;

    [SerializeField]
    public Sprite _image;

    public int Id => id;

    public string Title => _title;
    public Sprite Image => _image;
}
