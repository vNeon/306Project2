#pragma strict
// Script to check if player is under water and changes rendering settings if underwater.
// Author : Priyankit Singh

var waterLevel : float;
private var isUnderwater : boolean;
private var underwaterColour : Color;
private var normColour : Color;
var redComponent : float = 0.22f;
var greenComponent : float = 0.65f;
var blueComponent : float = 0.77f;

function Start () {
	underwaterColour =  new Color(redComponent, greenComponent, blueComponent, 0.5f);
	normColour = new Color(0.5f, 0.5f, 0.5f, 0.5f);
}

function Update () {
	// Return if no change in status
	if((transform.position.y < waterLevel) == isUnderwater){
		return;
	}
	if(transform.position.y < waterLevel){
		isUnderwater = true;
	} else{
		isUnderwater = false;
	}
	if(isUnderwater){
		setUnderwater();	
	} else{
		setNormal();
	}
}

// Set renderSettings if player is underwater
function setUnderwater(){
	Debug.Log("Player is underwater");
	RenderSettings.fogColor = underwaterColour;
	RenderSettings.fogDensity = 0.03f;
}

// Set renderSettings if player not underwater
function setNormal(){
	Debug.Log("Player is not underwater");
	RenderSettings.fogColor = normColour;
	RenderSettings.fogDensity = 0.002f;
}