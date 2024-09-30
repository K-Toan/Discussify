import React from 'react';
import { View, Text, StyleSheet, TouchableOpacity } from 'react-native';
import { useNavigation } from '@react-navigation/native';
import { theme } from '../components/theme'; // Import the current theme

const tags = [
    { id: 1, label: 'Game Development' },
    { id: 2, label: 'Programming' },
    { id: 3, label: 'Unity' },
    { id: 4, label: 'League of Legends' },
    { id: 6, label: 'Web Development' },
    { id: 5, label: 'Strategy' },
    { id: 7, label: 'HTML' },
    { id: 8, label: 'CSS' },
];

export default function CommunitiesScreen() {
    const navigation = useNavigation();

    return (
        <View style={[styles.container, { backgroundColor: theme.background }]}>
            <Text style={[styles.title, { color: theme.text }]}>Explore Communities by Topic:</Text>
            <View style={styles.tagContainer}>
                {tags.map((tag) => (
                    <TouchableOpacity
                        key={tag.id}
                        style={[styles.tag, { backgroundColor: theme.iconActiveColor }]}
                        onPress={() => navigation.navigate('CommunityDetails', { tagName: tag.label })}
                    >
                        <Text style={[styles.tagText, { color: theme.text }]}>{tag.label}</Text>
                    </TouchableOpacity>
                ))}
            </View>
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        paddingLeft: 20,
        paddingRight: 20,
    },
    title: {
        fontSize: 14,
        fontWeight: 'bold',
        marginBottom: 10,
    },
    tagContainer: {
        flexDirection: 'row',
        flexWrap: 'wrap',
    },
    tag: {
        paddingTop: 4,
        paddingBottom: 6,
        paddingLeft: 7,
        paddingRight: 7,
        borderRadius: 20,
        margin: 5,
    },
    tagText: {
        fontWeight: 'semibold',
    },
});
