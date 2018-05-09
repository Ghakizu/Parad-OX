using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsAssignations : MonoBehaviour 
{
	//All the materials and sprites that have to be stocked somewhere

	public static Material FreezedMaterial;
	public static Material NormalMaterial;
	public static Sprite FistsSprite;
	public static Sprite KatanaSprite;
	public static Sprite KnifeSprite;
	public static Sprite FreezeSprite;
	public static Sprite AirWallSprite;
	public static Sprite EarthSpikeSprite;
	public static Sprite FireBallSprite;
	public static Sprite FlashSprite;
	public static Sprite HealSprite;
	public static Sprite HealthPotionSprite;
	public static Sprite SpeedPotionSprite;


	public void Start()
	{
		FistsSprite = (Sprite)Resources.Load("Prefabs/UI_Images/sword/sword");
	}
}
