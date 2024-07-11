extends Area3D

@export var player: CharacterBody3D
var isEntered = false

func _ready():
	self.connect("body_entered", _on_body_entered)
	Dialogic.signal_event.connect(DialogEnd)

func _on_body_entered(body: Node3D) -> void:
	if body == player:
		if !isEntered:
			player.inputEnabled = false
			isEntered = true
			Dialogic.start("Ruffito_inicio")
			

func DialogEnd(arg: String):
	if arg == "DialogEnd":
		player.inputEnabled = true
		print("dialogo termino")

