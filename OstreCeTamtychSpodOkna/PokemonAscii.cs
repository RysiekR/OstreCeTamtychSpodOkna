﻿public class PokemonAscii
{
    public static String GetPokemonAsciByName(String pokemonName)
    {
        Dictionary<String, String> mapAscii = new Dictionary<String, String>();

        String pikachu = @"
*%#=..........      .. .   ....  .      . ... 
.#@%-:-:.                        ..:-#@@@:    
..#@=::::=..  ..       .      ..--:::%@%.  ...
.  -*:::::-=..      ..      .:-:::::=@*....   
.   .=-:::::=:....:::::.....-:::::::=... .::-=
.     .--:::::=::::::::::::::::::--..:==:::::=
.       .:--:::::::::::::::::---.--=:::::::::-
.        ..=::::::::::::::::::+-::::::::::::-.
.       ..=::::::::::::::::::::-::::::::::::-.
.       ..-:-@*:+::::::::+:@*:--::::::::::::-.
.       ..+::*##:::::::::=##=:--:::::::+=...  
.       .-***-::::::::::::::-++*::=:..   ..   
.        -****=::::::::::::-****::::=..  ... .
.       ..:#**:::::::::::::-****::::::-..     
.          :==::::::::::::::-=-::-:::=-:.    .
.         .-::::-::::::::--:::=::-=...   ..   
.        .=::::::::::::::::::::+**=.          
.       .-::::::::::::--:::::::-+..      ..  .
.      .*::::::::::+::--:::::::::-.      ..   
.  ..--=:::-:::::::=::-:::::::=::-:......     
.  .:=-:-::-:::::::-::+::::::+::-=--=.        
.   ..===-::=::::::===+:::::-:::-===..        
.     .:+====+-::::*++=::::+=--===...         
.. .. . .-==-----=-...:-:--=+===..   .  .     ";


        String snail = @"
                                       +=:     :==
                                       -:       ::
                                        --     +  
              ..:::.                     +:   #   
          -==+****#@@@#+:                .%  #-   
       .-**#@@@@@@%*++#@@@+.            :+@%@@-   
      -#@@@@%#**#%@@@@#=*@@@*         .#@@%#**#*. 
     *@@@*++#%@@%#++*@@@#-@@@@:      .%@@@*#++++* 
    *@@%-%@*+++*+++%%=#@@#=@@@@      #@%##*#*+**. 
   .@@@:@@-%*++++#@++@+*@@:@@@@+   -#%@*##%#--:   
   .@@%-@#=*-::=#++@++@:@@-#@@***#%%%@*@**#       
    %@@-@@-**=+=*%-@@-@-@@:@@++@%%%%#%#**@+       
    .@@@=*@#**+%%-%@**@-@=++=%%#@@#%*#%#*@-       
     .#@@%++***+*@@*+@+*=:+###%@%*%@@%#*@#        
       :+%@@@@@@@*+#@*#%+#%%@*@@@@##%##@+         
     .:-==***++=+*#%**##%%%%#*###**%%%+.          
.:-=+=---=+##%%#**##%##+-:-=*#####+=.       ";

        String bird = @"
                                 .""?`   ':_'      
                              'lj@j`""_rB$[.    .  
                          .""[M%*c|.@$$B{^`:+\xl   
            ``         'lj$Mz*z*.$$M***B$$$t,     
        .:{M@'      ""]#BzzzBBv..r##@$$$8c]^``.    
    .l{n$$$*(x_i.  }$@t#$$$$M..M$W#MMMM%$$ni.     
   `r{|j&$(rnv*{)  .j$vM$$$$@c.rc@$$$M#\:.     .`^
 .&*x#cB$$|j*cjzn)f^.*$n&$$$c*xfn###M@$*{,. `<\vc.
  .)""^,<j|j*r*zvcr|f,'&$r8$$*xzuu#WWz[^  ,)f#v8%' 
        nvnccznnt-]*t]u$%\$$*rjr*x/{<,'  ))ncMM^  
       )$jW/*[|ur*vvB8zzW$Bcfffu*j>'    '}i_\j[   
      ,$z/z$\c\cc8W**8B%$Wuftnn\/i,'    +>        
      ]c@j$#z$W*u\&&&vfn?}|zMB$#jrtnf;. .;}:.     
      .f$jMz$B***8nx)jjx#cz8$&u&$$n{`<rl'  ;t     
        <)f***#ux#$$$$$$@nj({/###%$u~  ""_1[),     
        .lnxx&*c&j11~:I##@$Bx,:z$8##vri""          
       `|&$$z]..       :n##x1`  ""?{/u##|(`        
      >t**z+.            ,*#u{""       `?$nI       
  ^"",v)M%_.          .""^,W&x;.        ^l/8z(.     
;t/j\rn,            ;}\xx/,         .{)f|fu{   ";

        String goose = @"
                                 -. -+.           
         -  :*                 :%=.##  :          
        =* -%+              +- @%.@@.-%.          
      +-=@-*%+           #:.@+-@*#@%:@=           
    + :@+#+@**         .-#%:@@=@#%@##@.           
  .-:%*+#**##*-      -.-@=#@*@##*+#+@-            
  .+%*#%%+####+.    :-%*##%==*==%#*=-             
  -#*#%%+=###*#+.    %##*=#+%##*=::-              
  :*#%%#=+##*##=@: :##%+*###*==:==.               
   :+#%%*-####@.%@*-**-+*##++=+=                  
    :+*##+:*##%#.%@@*+=#%#++++                    
    .=*#%%#=-*###:+@@@%*+++#-            .-+*=.   
      :+#%%#+:+##%+.+%@@@%**....:::====+=%##-*@%+-
        .=*###=+*##%+::+#%@#-==%%%%#+=::..-=:..   
           :+#%*==+###%**+.  :+###:               
             ..-*#****#@*.  .:.--                 
             -++===++-:. . ::-+:                  
          :+%####**+ : :-:--==.                   
       :-+**+*#+= -:-:---++=.                     
  :-+*#*****#%*-.-:=*=--:                         
:**%##%%**=--+==-*%%=                             
       .---+*:. -#=.                              
      -*%#=   .-#.                                
      ..   .+%@=           ";

        String octopus = @"
                     =#@@@%#-                     
                   :@%%@@@@%%%.                   
       .           %%=@@@@@@=@#           .       
      =.           @#:*@@@@*:%%           :=      
      ++           +@=-#@@#:+@=           +=      
       :**:         +@@@@@@@@=         -**:       
         +%:        -@@@@@@@@:        -%=         
         =#+       :@@@@@@@@@%.       *#-         
   :==-: =%#-    .+@%@@@@@@@@%@+.    =#@: :-==:   
 -+-::=*%*@%#%++#%*#@%=@@@@=@@**@#+*%#@%+%#=::-+: 
 : ..   =%%###***#@@* +@#%@=.*@@***####%#-   .. : 
.::-=#=   :=+#%@@@*-* @@=+@%.*-#@@@%#*=.  .=#=-::.
      *%+==+*%%*+=#@=+@%..@@=*@#++#%#+===+%+      
      :*@%###*#@@@@#=@@:*+-@@-#@@@@####%@@+.      
   .+@@#*===+@#=%@**@@-+@@==@@+*@%=%@+===*%@@+    
  -@%=    :*#+*@#*@@@=+@%%@=+@@@*%@*+#*:    +@@:  
 :@#    -*##@@%%%+#@*+@#  #@=#@#+%%@@%##*:   .%@. 
 +@.   #%@#--%%:  #@-@@    @@:@+  -%%-=#%%+   -@= 
 :@   #@%:  *@.   +@=@*    *@-@-   .@*  :@@+  .@: 
  +=  @@.  .@=     %+@*    #%+#     =@   :@@  =+  
   :. %@:-=+:      .@*@    @*@.      :*=-:@# .:   
      :@=      .    +#@.  :@#=    .      +@.      
       .*=    +     =%%    @%=     +    +*        
         .-.  ++. .=%%.    :%%=. .++  :-       ";

        String vulture = @"
                                     .:-:.         
                                    -***##*+=-.   
                                     ::#%+%#+#%=- 
                  +        :=:        +@@@###*=::=
                 #%      .#@:        -:*@%@%.     
                .@@=     %:#:        =-:+**=      
                .@#@*.   %==#-      .%#:====.     
                 #+:+**: +=#*+*:   -+*++--**-.    
           *-     *-*%++=--:+#**+--#+++++.=-*-    
           .@@#=:. :--++@**+-::-=-*++++**.-#:=:   
            .%@*=*++==-----+*##+==-+#+-++=-*+=.   
              -%%++*#**=*++*+=+==-==-=*++++*#*    
                .=***+++=+--+-+*#****#+*#+*@#     
                     :==--++-***+*%#**@*##@=      
                 :+#%%@#=%*==*@%#++#@+**#=        
               -#%@@#=::==--*+=::++**+=.          
             :=+#%*-:-**:=%+     .++-             
             -##*:-:*%-:#@*      +=%%%*-          
       :--=+%%#=:::@*:-@%@.      .  *=*=:         
         .-*%%:-.+@=:-@%%#          -.-           
-=  .  .-+%##:-.#@::-@#%%:                        
%+.  -+#%%#=-:+@#.:=%#.%=                         
:*%%%%%*+=:=*@#-::*#* +-                          
    ...:+*##*-:.:%#- .   ";

        String rhinoceros = @"
               =      .-=--+*+-                       
      .   :**-  =##=#+##**%**+=:. ..-:::.         
      *==-.+*#+*%***@%*******+*#*#=*#+*%##+       
=      -=+**-==%%%+-@#+**###+@%-*@%+=#=++++#*-    
=-      +*++%#%@%#*@**+*=:%+**@%+=%+%.+++=+##*#   
.*=.   -***=+*%@+%#%++%++*+@+=%%+=@#*=%+****#*:+  
 :##+==*%#=+=+%#%##+*%=+***++%=%-%@=%=#*#*###% +  
  .+*#*%%****#%%##%+#+##*#@@-#-*+*+*+@#**%**#+ =. 
    +***%@%%%##+--*#%=%+#**%%*+***#%%@#*####* =#@+
    +#*#%+-==:    =@%##*#*#*#%%%#*+++%#**@%*+  +@@
     :-=.          #*#*%##%          *%=+.##-+  .-
                   *%%-#++%         .*##. ##*     
                 .=%%@=%@@-        +#%##:+@%#     
                 .:=-=##%%#             -=++-     ";

        String scorpio = @"
      .---=+#%#+-              :+###*=---.       
     -+-=#%*=-:::--            --::::=*%#=:=-     
   .#=+@@=                              -%@+:#.   
  .@+#@@:   =:                       =    %@#-@:  
  *@+@@@--=*@     =*=        -*=     *%++=#@@+@%  
  %@#@%+--+@:    *#=          :##     #+--+%@@@@  
  #@@%*%@@+      #--  -    -. .**:     =%@#*%@@%  
  :@@@@*:        *=%- =*..*+::%=#        .+@@@@-  
.  .#%*#:         =%@%=*#*#-#@%=         .**%@:  .
#   -@@%++          :%%****#%-          =+#@@*   *
%-   :#@@**--=-     *%**++**%#     :=-:=*@@%-   :@
-@:    .=%**@+*##*--@%=:==:=%@+-**#**@#+%+.     %+
 -%+:     -#@@@%*##%+=-.*#.-=+%#**%@@%#-     :+#= 
  +@@#::::    -=+=*%=-.-@@=.-=##=++=.   .:..*@@#  
   .++#%#**=+##@#*@-+.#@#*@%:+-@*#@##*=*#%@@++:   
       .-=*#*=*=+*%=:#=-**-=#:*##+=*-***+=:       
            .#+#@%*# =#+===#=.#=%@@+*:            
        .---%%##+#*##-=%*+%+-*%=#=###%+--:        
      =#@@@#-.*=@%%.-*=+%%+=*+.*%@**.:-@@@%=      
    -#*++-..+@#%*.@::-*=---*+..@-+@*@+. :=+*#=    
   =@+    :*%*+-.*%* -*@@@@#=:=@*::+*%#:    -@*   
   @:    :##*. =*%#@%#+=---+#@@#@+-  *#*=    .%:  
  :-     #%@* -*#%==*:+****+-=*-##*: =@%+     .=  
  .      ##:  **@.  =%=-:::=#+   %#*  :%#      .  ";

        String chameleon = @"
                   ::=++**+==:. =%%*=.            
                -*#**++==+-++**#*-#@@@#-:-=-:.    
              -%#+*#####%#%**+*+=+##*+=+++***++-  
            .*%###***#**++++#%%*++=-***+*   +*+=+=
        :=-=#%++@#*+########+*++++**##@@%##***###+
    :+++%#+=#%#*##%+=+##**#%%*##*%%%%###%%%@%#*=. 
  =+=#*+*+=--=*##*#%**#*+%*%*+++%*+*#=====-.      
.#*#*-. ..      :*##%***+#*++-:#*#=:              
#**= -#*+*%#:     =*#.         %+=                
%+*  @+:-+.%%      *#+-:       ###-               
+**= -#**+=##       =#@#-       *@*=              
 -#%#++++%%+.         :.             ";

        String bear = @"
                          .-+++==+=-::=+:.   =:    
          .-=+***++++*=*%%*-.+##**#=+=@#*=+#++    
       -++===:.-+=#%#=*#+=-=*%*:+@+*#-+*+-+*#*=   
     =@%**#*++*=:@*%*@#=+#=:%@+*%:%@*-#@*:%%#=#=  
   :#@%%##@#**++#=+-%*-+@-#-%+:*#=#@#.#@-:%%#* +  
  +=%@#=+# -#**+*%++#=:==:@**#=+%*-#%+-%@+-*+  :+.
 +#:*#+-#*:@:#%+#@-@=**+***%=:%==%@+*+*+-*@@=  .#*
:@@+**+**#-##+*+@@-@==+=--+*#*-#@--#*#%#*+=+#+=-: 
#-@++*++**=++*#%@%=++-*=++*:#@%=+@@-**#=          
#:#%#*+**+%=#@%@+%@.@.#-%-#.*@#@:*%+#%*           
#:@@+#=#=#*+#.*.--*+@+-+++-+##==-=*#+=#=          
%##*%+*+++@-##%%@@@-@#%..*@+%.:-=**====-          
+#+.#%*@@@+**+#+#=  @**%%=@+:   #@=%++=           
.*+@*+%*=.*#%%*+%   *+=+:++=    .@=@+=+           
 *-+=**   .#=#*+=    *+=++*      *-%%%+           
-@*+-%     -@%*+=    :%==*%      .@==+#-          
%@*+#%.    .@%%@@%+:  =*%%++      :@@%@%#=.       
-@@@@@%=    :====:::  .@%%%@#*=     .:-.::.   ";



        mapAscii.Add("Pikachu", pikachu);
        mapAscii.Add("Charmander", snail);
        mapAscii.Add("Bulbazaur", bird);
        mapAscii.Add("Squirtle", goose);
        mapAscii.Add("Snali", snail);
        mapAscii.Add("Tentacruel", octopus);



        return mapAscii.GetValueOrDefault(pokemonName, scorpio);

    }
}
