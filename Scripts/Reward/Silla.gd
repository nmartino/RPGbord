extends StaticBody3D
@onready var spriteNode = $Sprite3D
@onready var areaNode = $Area3D
@onready var icon = $InteractiveIcon
@export var player: CharacterBody3D

var canPress: bool
var alredyTalked: bool

func _ready():
	areaNode.body_entered.connect(func(player):
		if(!alredyTalked):
			icon.visible = true
			canPress = true)
	areaNode.body_exited.connect(func(player):
		icon.visible = false
		canPress = false)
	Dialogic.signal_event.connect(FinalEnding)
		
func _input(event):
	if(canPress):
		if(event.is_action_pressed("Interact")):
			if(!alredyTalked):
				player.inputEnabled = false
				alredyTalked = true
				Dialogic.start("Final_Inicio")
		
func FinalEnding(arg: String):
	if arg == "FinalEnding":
		player.inputEnabled = true
