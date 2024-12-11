extends StaticBody3D
@onready var spriteNode = $Sprite3D
@onready var areaNode = $Area3D
@onready var icon = $InteractiveIcon
@export var player: CharacterBody3D
@export var reward: Resource
@export var chamberArea: BMUChamberArea

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
	Dialogic.signal_event.connect(BlueManEnding)
		
func _input(event):
	if(canPress && chamberArea == null):
		if(event.is_action_pressed("Interact")):
			if(!alredyTalked):
				player.inputEnabled = false
				alredyTalked = true
				Dialogic.start("BlueMan_inicio")
		
func BlueManEnding(arg: String):
	if arg == "BlueManEnding":
		player.inputEnabled = true
		
			

