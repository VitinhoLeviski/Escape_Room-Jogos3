using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
	public bool requiredItem;

	public bool grabbable;

	public bool destroy1;
	public bool destroy2;
	public bool destroy3;
	public bool picture1;
	public bool picture2;
	public bool picture3;

	public string text;
	public Sprite image;

	[Header("Inventory")]
	public bool InventoryItem;
	public string collectMessage;

}