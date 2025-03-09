#!/usr/bin/env python3
import os
import glob
import re

def add_missing_usings(file_path):
    """Add missing using directives to a file"""
    with open(file_path, 'r', encoding='utf-8-sig') as f:
        content = f.read()
    
    # Check if the file has the needed using directives
    missing_usings = []
    
    if 'using System.Net;' not in content:
        missing_usings.append('using System.Net;')
    
    if 'using Microsoft.VisualStudio.TestTools.UnitTesting;' not in content:
        missing_usings.append('using Microsoft.VisualStudio.TestTools.UnitTesting;')
        
    # Check if List<> is used but System.Collections.Generic is not imported
    if 'List<' in content and 'using System.Collections.Generic;' not in content:
        missing_usings.append('using System.Collections.Generic;')
    
    if len(missing_usings) == 0:
        print(f"No missing usings in {os.path.basename(file_path)}")
        return False
    
    # Find the namespace line to insert usings after it
    namespace_match = re.search(r'namespace\s+[^;]+;', content)
    if not namespace_match:
        print(f"Warning: No file-scoped namespace found in {os.path.basename(file_path)}")
        return False
    
    namespace_line = namespace_match.group(0)
    
    # Insert missing usings after the namespace line
    new_content = content.replace(
        namespace_line, 
        namespace_line + '\n' + '\n'.join(missing_usings)
    )
    
    # Write the modified content back to the file
    with open(file_path, 'w', encoding='utf-8') as f:
        f.write(new_content)
    
    print(f"Added missing usings to {os.path.basename(file_path)}")
    return True

def main():
    # Get all .cs files in the IPNetworkTest directory
    directory_path = os.path.join('src', 'TestProject', 'IPNetworkTest')
    test_files = glob.glob(os.path.join(directory_path, '*.cs'))
    
    # Process each file
    fixed = 0
    for file_path in test_files:
        if add_missing_usings(file_path):
            fixed += 1
    
    print(f"Added missing usings to {fixed} of {len(test_files)} files.")

if __name__ == "__main__":
    main()