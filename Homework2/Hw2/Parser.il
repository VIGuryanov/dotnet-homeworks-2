.class public abstract sealed auto ansi beforefieldinit
  Hw2.Parser
{
    .method public static void ParseCalcArguments(
        string[] args,
        [out] float64& val1,
        [out] valuetype [Hw2]Hw2.CalculatorOperation& operation,
        [out] float64& val2)
    {
        ldarg.0 
        call bool [HW2]Hw2.Parser::IsArgLengthSupported(string[])
        brfalse.s UnSuppLen

        ldarg.0
        ldc.i4.0
        ldelem.ref   
        ldarg.1      
        call bool [System.Runtime]System.Double::TryParse(string, float64&)
        brfalse.s ParseFailed

        ldarg 2        
        ldarg.0
        ldc.i4.1
        ldelem.ref 
        call valuetype [Hw2]Hw2.CalculatorOperation [Hw2]Hw2.Parser::ParseOperation(string)
        stind.i4 
        
        ldarg.0
        ldc.i4.2
        ldelem.ref    
        ldarg.3      
        call bool [System.Runtime]System.Double::TryParse(string, float64&)
        brfalse.s ParseFailed

        ret
        
        UnSuppLen:
        ldstr "Invalid count of input arguments"        
        newobj instance void [System.Runtime]System.ArgumentException::.ctor(string)
        throw
        
        ParseFailed: 
        ldstr "Wrong request syntax or unsupported type of values given. Format: {value operation value}. As values can be entered all integer or fractional numbers"       
        newobj instance void [System.Runtime]System.ArgumentException::.ctor(string)
        throw
    }
    
    .method private static bool IsArgLengthSupported (string[] args) cil managed
    {
        ldarg.0         
        ldlen   
        ldc.i4.3       
        ceq  
        ret
    }
    
    .method private static valuetype [Hw2]Hw2.CalculatorOperation ParseOperation(string arg)
    {
        ldarg.0
        ldstr "+"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s Plus
        
        ldarg.0
        ldstr "-"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s Minus
        
        ldarg.0
        ldstr "*"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s Multiply
                
        ldarg.0
        ldstr "/"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s Divide
        
        ldstr "Unsupported operation given"
        newobj instance void [System.Runtime]System.InvalidOperationException::.ctor(string)
        throw
                
        Plus:
        ldc.i4.0
        ret
        
        Minus:
        ldc.i4.1
        ret
        
        Multiply:
        ldc.i4.2
        ret
        
        Divide:
        ldc.i4.3
        ret
    }
}


