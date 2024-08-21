extends Area3D

@export var player: CharacterBody3D
var BuhoEntered = false

func _ready():
	self.connect("body_entered", _on_body_entered)
	Dialogic.signal_event.connect(DialogEndBuho)

func _on_body_entered(body: Node3D) -> void:
	if body == player:
		if !BuhoEntered:
			player.inputEnabled = false
			player.direction = Vector2.ZERO
			BuhoEntered = true
			Dialogic.start("Africano_Inicio")
			

func DialogEndBuho(arg: String):
	if arg == "DialogEndBuho":
		player.inputEnabled = true
		print("dialogo termino")
