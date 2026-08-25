extends Control

onready var _animated_sprite = $AnimatedSprite


func _ready():
	_animated_sprite.play("Unselected")


func _on_Control_mouse_entered():
	_animated_sprite.play("Hover")


func _on_Control_mouse_exited():
	_animated_sprite.play("Unselected")


func _on_Control_pressed():
	_animated_sprite.play("Click")
