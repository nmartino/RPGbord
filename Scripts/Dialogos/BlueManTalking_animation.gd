extends DialogicPortrait

@onready var anim = $AnimatedSprite2D

func _ready():
	anim.play("talk")
