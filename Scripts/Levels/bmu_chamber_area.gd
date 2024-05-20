class_name BMUChamberArea
extends Area3D

@export_category("Required Nodes")
@export var camera: Camera3D
@export var player: CharacterBody3D

@export_category("Behavior Variables")
@export var size: Vector3
@export var camera_animation_duration: float = 0.8

@onready var collision_shape: CollisionShape3D = $CollisionShape3D
@onready var camera_marker: Marker3D = $Marker3D


func _ready() -> void:
	collision_shape.shape.size = size
	camera_marker.position.y += size.y / 2
	camera_marker.position.z += size.z / 2
	


func _on_body_entered(body: Node3D) -> void:
	if body == player:  # NOTE: no puedo acceder a la class Player desde acá. Debería ser eso.
		var tween = create_tween().set_parallel(true)
		tween.set_trans(Tween.TRANS_SINE)
		tween.tween_property(
			camera, "global_position", camera_marker.global_position, camera_animation_duration
		)
		# NOTE: Para que se deje de mover.
		camera.reparent(camera_marker, true)
		pass
		# TODO: Move camera.
		# TODO: tween it.
		# TODO: clamp player movement.
