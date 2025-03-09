#!/usr/bin/env python3
import os
import glob
import re

def reorder_namespace_and_usings(file_path):
    """Move namespace declaration before using declarations in a file"""
    with open(file_path, 'r', encoding='utf-8-sig') as f:
        content = f.read()
    
    # Find the copyright header if it exists
    copyright_match = re.search(r'(^.*?<\/copyright>\s*?\n)', content, re.DOTALL)
    copyright_header = copyright_match.group(1) if copyright_match else ""
    
    # Find all using declarations
    using_declarations = re.findall(r'using\s+[^;]+;', content)
    if not using_declarations:
        print(f"No using declarations found in {file_path}")
        return False
    
    # Find the namespace declaration and content
    namespace_match = re.search(r'namespace\s+([^{]+)\s*{(.*)}', content, re.DOTALL)
    if not namespace_match:
        print(f"Warning: No namespace found in {file_path}")
        return False
    
    namespace_name = namespace_match.group(1).strip()
    namespace_content = namespace_match.group(2).strip()
    
    # Compose the new file content with namespace before usings
    new_content = copyright_header + "\n"
    new_content += f"namespace {namespace_name}\n{{\n"
    new_content += "    " + '\n    '.join(using_declarations) + '\n\n'
    new_content += namespace_content + "\n}"
    
    # Write the modified content back to the file
    with open(file_path, 'w', encoding='utf-8') as f:
        f.write(new_content)
    
    print(f"Updated {os.path.basename(file_path)}")
    return True

def main():
    # Get all .cs files in the IPNetworkTest directory
    directory_path = os.path.join('src', 'TestProject', 'IPNetworkTest')
    test_files = glob.glob(os.path.join(directory_path, '*.cs'))
    
    # Process each file
    successful = 0
    for file_path in test_files:
        if reorder_namespace_and_usings(file_path):
            successful += 1
    
    print(f"Successfully updated {successful} of {len(test_files)} files.")

if __name__ == "__main__":
    main()