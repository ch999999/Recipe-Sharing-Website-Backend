using RecipeSiteBackend.Models;

namespace RecipeSiteBackend.Validation
{
    public class RecipeValidator
    {
        public static ValidationError? validateInput(Recipe recipe)
        {
            if (string.IsNullOrEmpty(recipe.Title))
            {
                return new ValidationError()
                {
                    ErrorField = "title",
                    Message = "Required"
                };
            }
            else
            {
                if(title_valid(recipe.Title)==false)
                {
                    return new ValidationError()
                    {
                        ErrorField = "title",
                        Message = "Invalid"
                    };
                }
            }


            if (string.IsNullOrEmpty(recipe.Description))
            {
                return new ValidationError()
                {
                    ErrorField = "description",
                    Message = "Required"
                };
            }
            else
            {
                if(description_valid(recipe.Description)==false)
                {
                    return new ValidationError()
                    {
                        ErrorField = "description",
                        Message = "Invalid"
                    };
                }
            }


            if(prep_time_valid(recipe.Prep_Time_Mins)==false) 
            {
                return new ValidationError()
                {
                    ErrorField = "prep_time",
                    Message = "Required"
                };
            }

            if (cook_time_valid(recipe.Cook_Time_Mins)==false)
            {
                return new ValidationError()
                {
                    ErrorField = "cook_time",
                    Message = "Required"
                };
            }

            if (servings_valid(recipe.Servings) == false)
            {
                return new ValidationError()
                {
                    ErrorField = "servings",
                    Message = "Required"
                };
            }

            if (recipe.Notes != null)
            {
                foreach(Note note in recipe.Notes)
                {
                    if (string.IsNullOrEmpty(note.Description))
                    {
                        return new ValidationError()
                        {
                            ErrorField = "notes",
                            Message = "Note cannot be empty",
                            Index = note.Note_Number
                        };
                    }
                    else
                    {
                        if(note_valid(note.Description)==false)
                        {
                            return new ValidationError()
                            {
                                ErrorField="notes",
                                Message="Note is invalid",
                                Index= note.Note_Number
                            };
                        }
                    }
                }
            }

            if (recipe.Instructions == null)
            {
                return new ValidationError()
                {
                    ErrorField="instructions",
                    Message = "Must have at least one instruction"
                };
            }
            else
            {
                foreach(Instruction instruction in recipe.Instructions)
                {
                    if (string.IsNullOrEmpty(instruction.Description))
                    {
                        return new ValidationError()
                        {
                            ErrorField = "instructions",
                            Message = "instruction cannot have no text",
                            Index = instruction.Sequence_Number
                        };
                    }
                    else
                    {
                        if (instruction_valid(instruction.Description) == false)
                        {
                            return new ValidationError()
                            {
                                ErrorField = "instructions",
                                Message = "Instruction is invalid",
                                Index = instruction.Sequence_Number
                            };
                        }
                    }
                }
            }

            if (recipe.Ingredients == null)
            {
                return new ValidationError()
                {
                    ErrorField = "ingredients",
                    Message = "Must have at least one ingredient",
                };
            }
            else
            {
                foreach(Ingredient ingredient in recipe.Ingredients)
                {
                    if (string.IsNullOrEmpty(ingredient.Description))
                    {
                        return new ValidationError()
                        {
                            ErrorField = "instructions",
                            Message = "Ingredient must have text",
                            Index = ingredient.Ingredient_Number
                        };
                    }
                    else
                    {
                        if(ingredient_valid(ingredient.Description) == false)
                        {
                            return new ValidationError()
                            {
                                ErrorField = "ingredients",
                                Message = "Ingredient is invalid",
                                Index = ingredient.Ingredient_Number
                            };
                        }
                    }
                }
            }

            return null;
            
        }


        public static bool ingredient_valid(string ingredient)
        {
            if (string.IsNullOrEmpty(ingredient))
            {
                return false;
            }
            var trimmed = ingredient.Replace(" ", "");
            if(trimmed.Length <5 || ingredient.Length>800)
            {
                return false;
            }
            return true;
        }

        public static bool instruction_valid(string instruction)
        {
            if (string.IsNullOrEmpty(instruction))
            {
                return false;
            }
            var trimmed = instruction.Replace(" ", "");
            if(trimmed.Length<5 || instruction.Length > 1000)
            {
                return false;
            }
            return true;
        }

        public static bool note_valid(string note)
        {
            if (string.IsNullOrEmpty(note))
            {
                return false;
            }
            var trimmed = note.Replace(" ", "");
            if (trimmed.Length < 5 || note.Length > 800)
            {
                return false;
            }
            return true;
        }

        public static bool description_valid(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                return false;
            }
            var trimmed = description.Replace(" ", "");
            if(trimmed.Length<5 || description.Length > 3000)
            {
                return false;
            }
            return true;
        }

        public static bool title_valid(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return false;
            }
            var trimmed = title.Replace(" ", "");
            if (trimmed.Length < 3 || title.Length > 50)
            {
                return false;
            }
            return true;
            
        }

        public static bool prep_time_valid(int preptime)
        {
            if(preptime <= 0)
            {
                return false;
            }
            return true;
        }

        public static bool cook_time_valid(int cooktime)
        {
            if(cooktime <= 0)
            {
                return false;
            }
            return true;
        }

        public static bool servings_valid(int servings)
        {
            if (servings <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
