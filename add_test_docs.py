#!/usr/bin/env python3
import os
import re
import glob

def add_xml_documentation(file_path):
    with open(file_path, 'r', encoding='utf-8-sig') as f:
        content = f.read()

    # Regex to find public test methods without existing documentation
    method_pattern = r'(\s+)(?!\s+///\s*<summary>)(\[TestMethod\](?:\s+\[(?:TestCategory|ExpectedException)\([^\)]+\)\])*\s+public\s+void\s+(\w+)\s*\([^\)]*\)\s*\{)'
    
    # Use a function to generate documentation for each matched method
    def replace_method(match):
        indent = match.group(1)
        method_attrs = match.group(2)
        method_name = match.group(3)
        
        # Generate documentation based on method name
        doc_comment = generate_documentation(method_name, file_path)
        return f"{indent}/// <summary>\n{indent}/// {doc_comment}\n{indent}/// </summary>\n{indent}{method_attrs}"
    
    # Process the content with the regex
    modified_content = re.sub(method_pattern, replace_method, content)
    
    # Count how many methods were updated
    original_methods = len(re.findall(r'\[TestMethod\]', content))
    updated_methods = len(re.findall(r'/// <summary>', modified_content)) - len(re.findall(r'/// <summary>', content))
    
    # Write the modified content back to the file
    with open(file_path, 'w', encoding='utf-8') as f:
        f.write(modified_content)
    
    return updated_methods, original_methods

def generate_documentation(method_name, file_path):
    """Generate appropriate documentation for a test method."""
    
    # Extract the test class name from the file path
    class_name = os.path.basename(file_path).replace('.cs', '')
    
    # Extract the main feature being tested from the class name
    feature_match = re.match(r'IPNetwork_(.+)_Tests', class_name)
    feature = feature_match.group(1) if feature_match else "Unknown"
    
    # Convert feature to more readable form
    feature = re.sub(r'([a-z])([A-Z])', r'\1 \2', feature)
    
    # Special handling for common test method patterns
    if method_name.startswith('Test'):
        # Convert TestXxxYyy to "Tests Xxx Yyy"
        clean_name = method_name[4:]  # Remove 'Test' prefix
        readable_name = re.sub(r'([a-z])([A-Z])', r'\1 \2', clean_name)
        return f"Tests {feature} functionality with {readable_name}."
    
    # Handle other patterns
    readable_name = re.sub(r'([a-z])([A-Z])', r'\1 \2', method_name)
    
    # Create more specific documentation based on method name patterns
    if "Null" in method_name or "ANE" in method_name:
        return f"Tests {feature} with null input to ensure proper null handling."
    elif "Invalid" in method_name:
        return f"Tests {feature} with invalid input to ensure proper error handling."
    elif re.search(r'\d+$', method_name):  # Ends with number (e.g., TestToCidr32)
        match = re.search(r'(\d+)$', method_name)
        num = match.group(1)
        return f"Tests {feature} functionality with a /{num} network."
    else:
        return f"Tests {feature} functionality."

def main():
    # Get all test files in the IPNetworkTest directory
    test_files = glob.glob(os.path.join("src", "TestProject", "IPNetworkTest", "*.cs"))
    
    total_updated = 0
    total_methods = 0
    
    for file_path in test_files:
        file_name = os.path.basename(file_path)
        updated, methods = add_xml_documentation(file_path)
        total_updated += updated
        total_methods += methods
        print(f"Processed {file_name}: Added documentation to {updated} of {methods} methods")
    
    print(f"Documentation added to {total_updated} of {total_methods} methods across {len(test_files)} files.")

if __name__ == "__main__":
    main()