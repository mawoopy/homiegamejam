extends Panel


# Called when the node enters the scene tree for the first time.
func _ready():
	print("ready") # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
func _on_Play_pressed():
	print("Button Pressed")
	get_tree().change_scene("res://Scenes/UI/PressPlayTest.tscn")
	

func _on_Options_pressed():
	pass # Replace with function body.


func _on_Credits_pressed():
	pass # Replace with function body.


func _on_Quit_pressed():
	get_tree().quit()
