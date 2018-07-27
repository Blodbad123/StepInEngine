
public class Translation_p
{
    /**
     *  @description
     *  This is a translation, used for texts in-game and title / description in
     *  the title overview. It contains two set of information a string with the
     *  translated text, and a string with the language that this applies to. In
     *  the long run, the language should not be trusted as a string but instead
     *  an enum or some stable datatype. Initially, it is however just as string
     *  and also, honestly if you feel that this data structure way too simple a
     *  setup for translations, I agree. I however, just had to put something in
     *  here, but obviously you could use something like IL or whatever you want
     *  @end
     */
    public string language                  = null;
    public string content                   = null;
}
