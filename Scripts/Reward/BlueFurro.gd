extends CharacterBody3D
@onready var spriteNode = $Sprite3D
@onready var areaNode = $Area3D
@onready var icon = $InteractiveIcon
@export var player: CharacterBody3D

var canPress: bool
var alredyTalked: bool

func _ready():
	areaNode.body_entered.connect(func(_player):
		if(!alredyTalked):
			icon.visible = true
			canPress = true)
	areaNode.body_exited.connect(func(_player):
		icon.visible = false
		canPress = false)
	Dialogic.signal_event.connect(BlueFurroEnding)
		
func _input(event):
	if(canPress):
		if(event.is_action_pressed("Interact")):
			if(!alredyTalked):
				player.inputEnabled = false
				alredyTalked = true
				Dialogic.start("BlueFurro_Inicio")
		
func BlueFurroEnding(arg: String):
	if arg == "BlueFurroEnding":
		player.inputEnabled = true
