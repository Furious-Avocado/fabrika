for scriptFile in `find . -iname "*.cs" -type f`
do
    shouldWrapFile=false
    usings=( 'Sirenix' 'FuriousAvocado' )

    for using in "${usings[@]}"; do
        if grep -q -wF -e "$using" "$scriptFile"; then
            shouldWrapFile=true
        fi
    done

    if $shouldWrapFile ; then
        echo '#if ODIN_INSPECTOR && ODIN_INSPECTOR_3 && ODIN_VALIDATOR' > tmp.txt
        cat $scriptFile >> tmp.txt
        echo $'#endif' >> tmp.txt
        mv tmp.txt $scriptFile
    fi
done