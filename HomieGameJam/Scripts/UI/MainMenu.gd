extends Panel

onready var credits_panel = get_node("CreditsBG")

func _ready():
	print("ready")
	credits_panel.visible = false
	 # Replace with function body.
#	if credits_panel == false:
#		print("False")

func _on_Play_pressed():
	print("Button Pressed")
	get_tree().change_scene("res://Scenes/UI/PressPlayTest.tscn")
	

func _on_Options_pressed():
	pass # Replace with function body.


func _on_Credits_pressed():
	print("credits")
	credits_panel.visible = true
	
func _on_Back_pressed():
	print("back")
	credits_panel.visible = false


func _on_Quit_pressed():
	get_tree().quit()
