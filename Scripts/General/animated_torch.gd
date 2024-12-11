extends StaticBody3D
@export var piedra1: Node3D
@export var piedra2: Node3D
@onready var areaNode = $Area3D
@onready var icon = $InteractiveIcon
@export var player: CharacterBody3D
@export var animPlayer: AnimationPlayer


var canPress: bool
var alredyTalked: bool = false

func _ready():
	areaNode.body_entered.connect(func(_player):
		if(!alredyTalked):
			icon.visible = true
			canPress = true)
	areaNode.body_exited.connect(func(_player):
		icon.visible = false
		canPress = false)
	animPlayer.animation_finished.connect(_on_animation_player_animation_finished)
	
			
func _input(event):
	if(canPress):
		if(event.is_action_pressed("Interact")):
			if(!alredyTalked):
				animPlayer.play("girar")
				alredyTalked = true
				

func _on_animation_player_animation_finished(_anim_name):
	piedra1.PlayBrokenStone()
	piedra2.PlayBrokenStone()
