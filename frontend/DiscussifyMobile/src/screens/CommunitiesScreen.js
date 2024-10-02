import React from 'react';
import { View, Text, StyleSheet, Image, TouchableOpacity, FlatList } from 'react-native';
import { Avatar, Card, Button } from 'react-native-paper';
import { useNavigation } from '@react-navigation/native';
import { theme } from '../components/theme';

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

const communities = [
    { id: 1, name: 'Community 1', avatar: 'https://picsum.photos/120', totalMember: 72200, description: 'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.' },
    { id: 2, name: 'Community 2', avatar: 'https://picsum.photos/650', totalMember: 1400, description: 'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.' }
]

export default function CommunitiesScreen() {
    const navigation = useNavigation();

    return (
        <View style={[styles.container, { backgroundColor: theme.background }]}>
            <View style={{ marginBottom: 10 }}>
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
            <View>
                <Text style={[styles.title, { color: theme.text }]}>Recommended:</Text>
                <FlatList
                    data={communities}
                    keyExtractor={(item) => item.id.toString()}
                    renderItem={({ item }) => (
                        <View style={{ margin: 10 }}>
                            <View style={{ flexDirection: 'row', alignItems: 'center', justifyContent: 'space-between', marginBottom: 8 }}>
                                <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                                    <Image
                                        source={{ uri: item.avatar }}
                                        style={{ width: 36, height: 36, borderRadius: 20, marginRight: 10 }}
                                    />
                                    <View style={{ flexDirection: 'column', alignItems: 'flex-start' }}>
                                        <Text style={{ fontWeight: 'bold', color: theme.text }}>
                                            {item.name}
                                        </Text>
                                        <Text style={{ marginLeft: 0, fontSize: 12, color: 'gray' }}>{item.totalMember} members</Text>
                                    </View>
                                </View>

                                <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                                    <View style={{ flexDirection: 'column', alignItems: 'flex-start' }}>
                                        <Button>Join</Button>
                                    </View>
                                </View>
                            </View>
                            <Text style={{ padding: 5, color: theme.subtext }} numberOfLines={2}>
                                {item.description}
                            </Text>
                        </View>
                    )}
                />
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
